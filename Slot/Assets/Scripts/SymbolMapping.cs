using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SymbolMapping", menuName = "Scriptable Objects/SymbolMapping")]
public class SymbolMapping : ScriptableObject
{
    [SerializeField]
    private List<Symbol> symbolList = new List<Symbol>();

    public Symbol GetSymbol(string name)
    {
        foreach (Symbol symbol in symbolList) 
        {
            if (symbol.name.Equals(name))
                return symbol;
        }

        Debug.Log($"Symbol {name} not found on mapping");
        return null;
    }

    public Symbol GetRandomSymbol()
    {
        int random = UnityEngine.Random.Range(0, symbolList.Count);
        return symbolList[random];
    }
}

[Serializable]
public class Symbol
{
    public string name;
    public Sprite sprite;
    public float size;
}