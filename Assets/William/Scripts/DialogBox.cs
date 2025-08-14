using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogBox : Code.Scripts.Managers.Singleton<DialogBox>
{
    private Text dialogBoxText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Sequence mySequence;
    int idMaker;
    int currentID;

    void Start()
    {
        dialogBoxText = GetComponent<Text>();
        mySequence = DOTween.Sequence();
        mySequence.Pause();
        idMaker = 0;
        currentID = 0;
    }

    public void DOText(string content)
    {
        mySequence.Append(dialogBoxText.DOText(content, 3f).SetId("dialogeText"+idMaker).OnComplete(()=>OnTweenComplete(idMaker))).OnComplete(()=>Reset());
        idMaker++;
    }

    public void Reset()
    {
        mySequence = DOTween.Sequence();
        mySequence.Pause();
    }

    public void KillText()
    { 
        DOTween.Kill("dialogueText" + currentID);
    }

    public void StartText()
    {
        mySequence.Play();
    }

    public void OnTweenComplete(int cid)
    {
        //GameManager.Instance.GetLevelInfo().GetDialogText().SetActive(false);
        currentID = cid;
    }

}
