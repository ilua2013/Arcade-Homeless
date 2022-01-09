using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakebale
{
    void StartMoveTo(Transform targetPath);
    IEnumerator MoveTo(Transform targetPath);
}
