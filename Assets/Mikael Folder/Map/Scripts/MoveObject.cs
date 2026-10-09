using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveObject : MonoBehaviour
{
    public enum SlideAxis
    {
        Forward, Backward, Up, Down, Left, Right
    }
    [SerializeField]
    SlideAxis slideDir = SlideAxis.Forward;
    public enum DoorOpenDirection 
    {
        Inwards, Outwards
    }
    [SerializeField]
    DoorOpenDirection doorRotation = DoorOpenDirection.Inwards;
    //InteractionRange
    [SerializeField]
    float maxDist = 3f;
    float dist;
    [SerializeField]
    float slideDistance = 1f;
    [SerializeField]
    float slideSpeed = 1f;

    Vector3 orignalPos;
    Vector3 targetPos;

    bool isMoved = false;
    bool isMoving = false;


    //Door
    [SerializeField]
    float openAngle = 90;
    [SerializeField]
    float openSpeed = 2f;
    [SerializeField]
    bool isDoor = false;
    bool isOpen = false;

    Quaternion closedRotation;
    Quaternion openRotation;

    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        orignalPos = transform.position;

        Vector3 dirVec = GetDirectionVector();
        targetPos = orignalPos + (dirVec * slideDistance);

        closedRotation = transform.rotation;
        float doorRot = GetDoorRotaion();
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, doorRot, 0));
    }
    private Vector3 GetDirectionVector()
    {
        switch (slideDir)
        {
            case SlideAxis.Forward:
                return transform.forward;
            case SlideAxis.Backward:
                return -transform.forward;
            case SlideAxis.Right:
                return transform.right;
            case SlideAxis.Left:
                return -transform.right;
            case SlideAxis.Up:
                return Vector3.up;
            case SlideAxis.Down:
                return Vector3.down;
            default:
                return transform.forward;
        }
    }
    private float GetDoorRotaion() 
    {
        switch (doorRotation) 
        {
            case DoorOpenDirection.Inwards:
                return openAngle;
            case DoorOpenDirection.Outwards:
                return -openAngle;
            default:
                return openAngle;
        }
    }
    private void OnMouseDown()
    {
        dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        if(dist <= maxDist) 
        {
            if (isMoving)
            {
                return;
            }

            isMoved = !isMoved;
            StopAllCoroutines();
            if (!isDoor)
            {
                StartCoroutine(SlideRoutine(isMoved ? targetPos : orignalPos));
            }
            else
            {
                StartCoroutine(DoorRoutine());
            }
        }
    
    }

    private IEnumerator SlideRoutine(Vector3 dest)
    {
        isMoving = true;
        rb.isKinematic = true;
        float elapsedTime = 0f;
        float length = Vector3.Distance(transform.position, dest);
        float totalTime = length / (slideSpeed * 2f);

        Vector3 startingPos = transform.position;


        while (elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(startingPos, dest, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = dest;
        rb.isKinematic = false;
        isMoving = false;
    }

    private IEnumerator DoorRoutine()
    {
        isMoving = true;
        rb.isKinematic = true;

        Quaternion targetRotation = isOpen ? closedRotation : openRotation;
        isOpen = !isOpen;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
        rb.isKinematic = false;
        isMoving = false;
    }
}
