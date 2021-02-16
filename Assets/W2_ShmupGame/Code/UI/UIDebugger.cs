using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIDebugger : MonoBehaviour
{
    public static UIDebugger instance;
    [SerializeField] Text A1;
    [SerializeField] Text A2;
    [SerializeField] Text A3;
    [SerializeField] Text B1;
    [SerializeField] Text B2;
    [SerializeField] Text B3;
    [SerializeField] Text C1;
    [SerializeField] Text C2;
    [SerializeField] Text C3;

    private void Awake()
    {
        instance = this;
        ResetA1();
        ResetA2();
        ResetA3();
        ResetB1();
        ResetB2();
        ResetB3();
        ResetC1();
        ResetC2();
        ResetC3();
    }

    public void SetA1(string text) => A1.text = text;
    public void ResetA1() => A1.text = "";
    public void SetA2(string text) => A2.text = text;
    public void ResetA2() => A2.text = "";
    public void SetA3(string text) => A3.text = text;
    public void ResetA3() => A3.text = "";
    public void SetB1(string text) => B1.text = text;
    public void ResetB1() => B1.text = "";
    public void SetB2(string text) => B2.text = text;
    public void ResetB2() => B2.text = "";
    public void SetB3(string text) => B3.text = text;
    public void ResetB3() => B3.text = "";
    public void SetC1(string text) => C1.text = text;
    public void ResetC1() => C1.text = "";
    public void SetC2(string text) => C2.text = text;
    public void ResetC2() => C2.text = "";
    public void SetC3(string text) => C3.text = text;
    public void ResetC3() => C3.text = "";
}