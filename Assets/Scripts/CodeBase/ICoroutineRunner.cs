using System.Collections;
using UnityEngine;

namespace Apocalypse.CodeBase
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator enumerator);
        void StopCoroutine(Coroutine coroutine);
    }
}