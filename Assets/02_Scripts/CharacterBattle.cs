using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    Character_Anims char_Anims;

    private State state;
    private Vector3 slideTargetPos;
    private Action onSlideComplete;
    private GameObject selectionCircleGameobject;
    private HealthSystem healthSystem;

    public bool isPlayerTeam;
    [SerializeField] Transform pHealthBar;

    private enum State
    {
        Idle,
        Sliding,
        Busy
    }

    private void Awake()
    {
        char_Anims = GetComponent<Character_Anims>();
        selectionCircleGameobject = transform.Find("SelectionCircle").gameObject;
        HideSelectionCircle();
        state = State.Idle;
    }

    private void Start()
    {
        
    }

    public void Setup(bool isPlayerTeam)        // Setup individual de cada Character invocado
    {
        //this.isPlayerTeam = isPlayerTeam;     // De que team es

        healthSystem = new HealthSystem(100);       // Fija la vida del Character
        Transform healthBarTransform = Instantiate(pHealthBar, char_Anims.transform.position + new Vector3(0f,2f,0f), Quaternion.identity);     // Fija la posicion de la barra
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();     // Busca la referencia del script
        healthBar.Setup(healthSystem);      //Configuera la vida de la BARRA basado en la vida del sistema.


        char_Anims.PlayAnimStarter();   //REEMPLAZAR POR IDLE ?
    }

    private void Update()       // ESTADOS DE COMBATE
    {
        switch (state)
        {
            case State.Idle:    //
                break;

            case State.Sliding:     // Deslizandose al ataque
                float slideSpeed = 5f;
                transform.position += (slideTargetPos - GetPosition()) * slideSpeed * Time.deltaTime;

                float reachedDistance = 1f;
                if (Vector3.Distance(GetPosition(),slideTargetPos) < reachedDistance)
                {
                    transform.position = slideTargetPos;
                    onSlideComplete();
                }

                break;

            case State.Busy:    //
                break;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Damage(int dmgAmount)
    {
        healthSystem.Damage(dmgAmount);

        if (healthSystem.IsDead())
        {
            // Animación de muerte
            Debug.Log("Se acabo el combate");
        }
    }

    public bool IsDead()
    {
        return healthSystem.IsDead();
    }

    public void Attack(CharacterBattle target,Action onAttackComplete)
    {
        Vector3 slideTargetPosition = target.GetPosition() + (GetPosition() - target.GetPosition()).normalized * 1.5f;
        Vector3 startPos = GetPosition();

        // 1. Nos deslizamos
        SlideToPosition(slideTargetPosition, ()=>
        {
            // 2. Llegamos y le pegamos
            state = State.Busy;
            char_Anims.PlayAnimAttack(() => //Action on HIT
            {
                target.Damage(25);
            }
                ,()=>   //Action on Complete
            {   
                // 3. Se completo el ataque, te devuelves
                SlideToPosition(startPos, () =>
                {
                    // 4. Te has devuelto, ahora pasas a Idle
                    state = State.Idle;
                    char_Anims.PlayAnimIdle(()=> { });
                    onAttackComplete();
                });
            });
        });

    }

    private void SlideToPosition(Vector3 slideTargetPos, Action onSlideComplete)
    {
        this.slideTargetPos = slideTargetPos;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
    }

    public void HideSelectionCircle()
    {
        selectionCircleGameobject.SetActive(false);
    }

    public void ShowSelectionCircle()
    {
        selectionCircleGameobject.SetActive(true);
    }
}
