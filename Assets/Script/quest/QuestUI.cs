using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public int index;

    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public TextMeshProUGUI objectiveList;
    public Button completeButton;

    public void SetValue(QuestTracker tracker, int idx)
    {
        index = idx;

        // ❌ ห้ามเปิด-ปิด Slot ด้วย code
        // gameObject.SetActive(true);

        if (title != null)
            title.text = tracker.questName;

        if (description != null)
            description.text = tracker.questDescription;

        UpdateObjectiveList(tracker);

        if (completeButton != null)
            completeButton.interactable = tracker.questCanComplete;
    }

    public void UpdateProgress(QuestTracker tracker)
    {
        UpdateObjectiveList(tracker);

        if (completeButton != null)
            completeButton.interactable = tracker.questCanComplete;
    }

    private void UpdateObjectiveList(QuestTracker tracker)
    {
        if (objectiveList == null)
        {
            Debug.LogWarning("ObjectiveList not assigned in QuestUI!");
            return;
        }

        objectiveList.text = "";  // ล้างก่อน

        if (tracker.objectives != null)
        {
            foreach (Objective obj in tracker.objectives)
            {
                objectiveList.text += $"{obj.targetID} : {obj.currentAmount}/{obj.requiredAmount}\n";
            }
        }
    }

    public void CompleteQuest()
    {
        QuestTracker tracker = QuestManager.instance.ongoingQuest[index];

        if (tracker != null && tracker.questCanComplete)
        {
            QuestManager.instance.CompleteQuest(index);
        }
        else
        {
            Debug.Log("Cannot complete quest yet!");
        }
    }

    public void CancelQuest()
    {
        if (QuestManager.instance != null)
            QuestManager.instance.CancelQuest(index);
    }

    public void ClearValue()
    {
        // ❌ ห้ามปิด Slot 
        // gameObject.SetActive(false);

        if (title != null)
            title.text = "";

        if (description != null)
            description.text = "";

        if (objectiveList != null)
            objectiveList.text = "";

        if (completeButton != null)
            completeButton.interactable = false;
    }
}
