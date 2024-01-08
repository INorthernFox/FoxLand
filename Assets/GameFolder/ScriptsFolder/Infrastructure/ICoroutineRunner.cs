using System.Collections;
using UnityEngine;

namespace GameFolder.ScriptsFolder.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}