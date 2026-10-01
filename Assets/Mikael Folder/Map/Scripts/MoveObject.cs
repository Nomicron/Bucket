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
    SlideAxis slideDire = SlideAxis.Forward;
    [SerializeField]
    float slideDistance = 1f;
    [SerializeField]
    float slideSpeed = 1f;

    Vector3 orignalPos;
    Vector3 targetPos;
    bool isMoved = false;
    bool isMoving = false;

    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Save orignal position
        orignalPos = transform.position;
        // Calculate position based on object's forward direction
        Vector3 direVec = GetDirectionVector();
        targetPos = orignalPos + (direVec * slideDistance);
    }
    private Vector3 GetDirectionVector()
    {
        switch (slideDire)
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
    private void OnMouseDown()
    {
        if (isMoving) 
        {
            return;
        }
        // Toggle the state (if it's at home, move it out; if it's out, move it back)
        isMoved = !isMoved;


        StopAllCoroutines();
        StartCoroutine(SlideRoutine(isMoved ? targetPos : orignalPos));
    }

    private IEnumerator SlideRoutine(Vector3 dest) 
    {
        isMoving = true;
        rb.isKinematic = true;
        float elapsedTime = 0f;
        float length = Vector3.Distance(transform.position, dest);
        float totalTime = length / (slideSpeed * 2f);

        Vector3 startingPos = transform.position;


        while(elapsedTime < totalTime)
        {
            transform.position = Vector3.Lerp(startingPos, dest, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = dest;
        rb.isKinematic = false;
        isMoving = false;
    }
}
