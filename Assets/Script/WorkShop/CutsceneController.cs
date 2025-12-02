using UnityEngine;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    public Camera cutsceneCamera;
    public Transform cutsceneStartPoint;
    public Transform lookAtTarget;
    public Transform bossTarget;

    public float rotateDuration = 2f;

    public GameObject bossObject;
    public Animator bossAnimator;

    public GameObject player;
    public MonoBehaviour playerMovementScript;


    public void PlayCutscene()
    {
        Debug.Log("🎬 เริ่มคัตซีน!");
        StartCoroutine(CutsceneSequence());
    }

    IEnumerator CutsceneSequence()
    {
        Debug.Log("🔒 ปิดคอนโทรลผู้เล่น");
        playerMovementScript.enabled = false;

        // 📌 ย้ายกล้องไปตำแหน่งเริ่มคัตซีน
        cutsceneCamera.transform.position = cutsceneStartPoint.position;
        cutsceneCamera.transform.rotation = cutsceneStartPoint.rotation;

        // 🎥 เปิดกล้องคัตซีน
        Debug.Log("🎥 เปิดกล้องคัตซีน");
        cutsceneCamera.gameObject.SetActive(true);

        // จ้องพื้นก่อน
        Debug.Log("📌 กล้องจ้อง lookAtTarget");
        cutsceneCamera.transform.LookAt(lookAtTarget);

        yield return new WaitForSeconds(0.5f);

        // หมุนขึ้นหาบอส
        Debug.Log("⬆ กล้องเงยขึ้นหา bossTarget");

        float timer = 0f;
        while (timer < rotateDuration)
        {
            timer += Time.deltaTime;

            Vector3 dir = Vector3.Lerp(
                (lookAtTarget.position - cutsceneCamera.transform.position),
                (bossTarget.position - cutsceneCamera.transform.position),
                timer / rotateDuration
            );

            cutsceneCamera.transform.rotation = Quaternion.LookRotation(dir);
            yield return null;
        }

        Debug.Log("👹 เปิดบอส!");
        bossObject.SetActive(true);

        if (bossAnimator != null)
            bossAnimator.SetTrigger("Appear");

        yield return new WaitForSeconds(1.5f);

        Debug.Log("🎮 กลับคอนโทรลผู้เล่น");
        cutsceneCamera.gameObject.SetActive(false);
        playerMovementScript.enabled = true;
    }
}
