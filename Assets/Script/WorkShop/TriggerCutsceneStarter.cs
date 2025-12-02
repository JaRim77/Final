using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject cutsceneObj;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player == null) return;

        cutsceneObj.SetActive(true);

        // ⭐ ให้มันเริ่มคัตซีนจริงๆ
        cutsceneObj.GetComponent<CutsceneController>().PlayCutscene();

        gameObject.SetActive(false);
    }

}
