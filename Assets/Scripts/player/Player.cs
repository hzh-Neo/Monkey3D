using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;



public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }


    [SerializeField] public Transform itemPosition;

    private KitchenItem kitchenItem;

    public class SelectCounter : EventArgs
    {
        public BaseCounter CC;
    }
    public event EventHandler<SelectCounter> onSelectChange;
    public Rigidbody body;
    public float speed = 10f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public float interaceDis = 2;

    public CapsuleCollider capsuleCollider;

    public LayerMask CountersLayerMask;

    [SerializeField]
    private float rotateSpeed = 30f;

    [SerializeField]
    private InputCotroller inputCotroller;

    private BaseCounter _selectCC;
    private BaseCounter selectCC
    {
        get
        {
            return _selectCC;
        }
        set
        {
            _selectCC = value;
            onSelectChange?.Invoke(this, new SelectCounter() { CC = value });
        }
    }


    private void Awake()
    {
        if (!capsuleCollider)
        {
            capsuleCollider = GetComponentInChildren<CapsuleCollider>();
        }
        if (Instance == null)
        {
            Instance = this;
        }
        inputCotroller.onInteraceEvent += InputCotroller_onInteraceEvent; ;
    }

    private void InputCotroller_onInteraceEvent(object sender, EventArgs e)
    {
        if (selectCC != null)
        {
            selectCC.interace(this);
            selectCC = null;
        }
    }

    private void listenMove()
    {
        Vector3 newMove = inputCotroller.getMoveVector();
        if (PlayerState.Instance.isGround && inputCotroller.isJump())
        {
            Vector3 jumpVector = new Vector3(0, Mathf.Sqrt(jumpHeight * -2f * gravity), 0);
            body.velocity = jumpVector;
        }
        PlayerState.Instance.isWalking = false;
        if (newMove.x != 0 || newMove.z != 0)
        {
            body.transform.forward = Vector3.Slerp(body.transform.forward, newMove, rotateSpeed * Time.deltaTime);
            bool isGuad = Physics.CapsuleCast(body.transform.position, body.transform.position + Vector3.up * capsuleCollider.height,
                capsuleCollider.radius, newMove, speed * Time.deltaTime);
            if (isGuad)
            {
                Vector3 canMoveX = new Vector3(newMove.x, 0, 0).normalized;
                isGuad = Physics.CapsuleCast(body.transform.position, body.transform.position + Vector3.up * capsuleCollider.height,
                    capsuleCollider.radius, canMoveX, speed * Time.deltaTime);
                if (isGuad)
                {
                    Vector3 canMoveZ = new Vector3(0, 0, newMove.z).normalized;
                    isGuad = Physics.CapsuleCast(body.transform.position, body.transform.position + Vector3.up * capsuleCollider.height,
                        capsuleCollider.radius, canMoveZ, speed * Time.deltaTime);
                    if (!isGuad)
                    {
                        newMove = canMoveZ;
                    }
                    else
                    {
                        newMove = Vector3.zero;
                    }
                }
                else
                {
                    newMove = canMoveX;
                }
            }

            if (!isGuad)
            {

                body.transform.position += newMove * speed * Time.deltaTime;
                PlayerState.Instance.isWalking = newMove != Vector3.zero;
            }

        }



        // 应用重力
        if (!PlayerState.Instance.isGround)
        {
            PlayerState.Instance.airY = body.velocity.y;
            body.velocity += Vector3.up * gravity * Time.deltaTime;
        }
    }

    private void listenInterace()
    {
        RaycastHit raycastHit = new();
        Vector3[] directions = { body.transform.forward, body.transform.right, -body.transform.right };
        bool hit = false;

        foreach (Vector3 direction in directions)
        {
            if (Physics.Raycast(body.transform.position, direction, out raycastHit, interaceDis, CountersLayerMask))
            {
                hit = true;
                break;
            }
        }

        if (hit)
        {
            if (raycastHit.transform.TryGetComponent<BaseCounter>(out BaseCounter cc))
            {
                if (selectCC == null || selectCC != cc)
                {
                    if (HasItem())
                    {
                        if (cc.getCounterType() == CounterType.clearCounter)
                        {
                            if (!cc.HasItem())
                            {
                                selectCC = cc;
                            }

                        }
                        else if (cc.getCounterType() == CounterType.cuttingCounter)
                        {
                            if (!getItem().IsSlice())
                            {
                                selectCC = cc;
                            }
                            else
                            {
                                selectCC = null;
                            }
                        }
                        else
                        {
                            selectCC = null;
                        }
                    }
                    else
                    {
                        if (cc.getCounterType() == CounterType.contaionCounter)
                        {
                            selectCC = cc;
                        }
                        else if (cc.getCounterType() == CounterType.clearCounter && cc.HasItem())
                        {
                            selectCC = cc;
                        }
                        else if (cc.getCounterType() == CounterType.cuttingCounter && cc.HasItem())
                        {
                            selectCC = cc;
                        }
                    }
                }
            }
            else
            {
                selectCC = null;
            }
        }
        else
        {
            selectCC = null;
        }

    }

    void Update()
    {
        listenMove();
        listenInterace();
    }

    public void ClearItem()
    {
        kitchenItem = null;
    }

    public Transform getTransform()
    {
        return itemPosition;
    }

    public bool HasItem()
    {
        return kitchenItem != null;
    }

    public KitchenItem getItem()
    {
        return kitchenItem;
    }

    public void setItem(KitchenItem item)
    {
        kitchenItem = item;
    }
}
