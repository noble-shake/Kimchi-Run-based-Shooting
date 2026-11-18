using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum SequenceMethods
{
    SeqObjectWalk,
    SeqDialPlay,
    SeqGenerate,
    SeqActive,
    SeqComposite,
    SeqCustom,
}

public class Sequences : MonoBehaviour
{
    public static Sequences Instance;

    [Header("Internal Setup")]
    [SerializeField] private bool queTrigger;
    public bool QueTrigger { get { return queTrigger; } set { queTrigger = value; } }

    protected Queue<IEnumerator> QueSeqeunce = new Queue<IEnumerator>();

    WaitForSecondsRealtime DelaySequencer = new WaitForSecondsRealtime(0f);
    bool SequenceProcessing;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected IEnumerator SequencePlay()
    {
        while (QueSeqeunce.Count > 0)
        {
            queTrigger = true;
            yield return StartCoroutine(QueSeqeunce.Dequeue());
            yield return new WaitUntil(() => queTrigger == false);
        }
        SequenceProcessing = false;

    }

    public void GenerateSequence(List<IEnumerator> methods, List<float> delays)
    {
        SequenceProcessing = true;
        int count = methods.Count;

        for (int inum = 0; inum < count; inum++)
        {
            QueSeqeunce.Enqueue(Task(methods[inum], delays[inum]));
        }
        queTrigger = true;

        StartCoroutine(SequencePlay());
    }

    public IEnumerator Task(IEnumerator _method, float delay)
    {
        DelaySequencer.waitTime = delay;
        yield return DelaySequencer;
        yield return StartCoroutine(_method);
    }

    // EXAMPLE Sequencer
    #region Test Methods
    public IEnumerator SeqObjectWalk(GameObject _object, Vector3? _pos = null, float speed = 100f)
    {
        while (Vector3.Distance(_object.transform.position, _pos.Value) > 0.001f)
        {
            _object.transform.position = Vector3.MoveTowards(_object.transform.position, _pos.Value, speed * Time.deltaTime);
            yield return null;
        }
        queTrigger = false;
    }

    public IEnumerator SeqActive(GameObject _object, bool _active)
    {
        _object.SetActive(_active);
        yield return null;
        queTrigger = false;
    }
    #endregion

}