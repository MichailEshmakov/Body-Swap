using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Body))]
[RequireComponent(typeof(Celebrator))]
[RequireComponent(typeof(Model))]
public class Character : MonoBehaviour
{
    private Body _body;
    private Celebrator _celebrator;
    private Model _model;

    public Body Body => _body;
    public Celebrator Celebrator => _celebrator;
    public Model Model => _model;

    private void Awake()
    {
        _body = GetComponent<Body>();
        _celebrator = GetComponent<Celebrator>();
        _model = GetComponent<Model>();
    }
}
