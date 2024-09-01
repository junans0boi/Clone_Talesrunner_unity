using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlay_PlayerMove : MonoBehaviourPun

{
    [Header("About Timer")]
    public Text _TimerText;
    float _Sec;
    int _Min;
    string Score;
    [Header("About Player")]
    Vector3 dir;
    public SinglePlay_GameManager GM;

    [Header("About Move")]
    public float moveSpeed = 8;     // 움직임 관련
    float startSpeed;               // 시작할때 움직임 속도 
    public float runSpeed = 16;     // 달리기 속도
    
    [Header("About CharacterController")]
    CharacterController cc;         // 캐릭터 컨트롤러 컴포넌트
    
    [Header("About Animator")]
    public Animator anim;           // 애니메이션 컴포넌트
    
    [Header("About Jump")]
    public float gravity = -9.81f;  // 중력
    public float jumpPower = 3;     // 점프 파워
    public float yVelocity;         // yVelocity
    public int jumpCount;           // 현재 점프 횟수
    public int maxjumpCount = 2;    // 최대 점프 횟수
    public bool isJumping;          // 지금 점프 중이니?

    [Header("About Damage")]
    public float curDamageEnergy = 0;   // 현재 데미지 게이지
    public float maxDamageEnergy = 10;  // 최대 데미지 게이지
    public float knockBackForce;        // 넉백되는 힘
    public float knockBackTime;         // 넉백되는 시간
    public float knockBackCounter;      
    public float curDamageTime;         // 현재 데미지 시간
    public float maxDamageTime = 10;    // 최대 데미지 시간
    
    [Header("About Dash")]
    public float curDashTime = 0;       // 현재 대쉬 상태 시간
    public float maxDashTime = 10;      // 최대 대쉬 상태 시간
    public bool isCanDash = false;      // 대쉬 가능을 판단하는 Bool
    
    [Header("About Run")]
    public float curRunEnergy = 100;    // 현재 달리기 에너지
    public float maxRunEnergy = 100;    // 최대 달리기 에너지
    
    
    
    [Header("About VunNo")]
    public float curVunNoTime;           // 현재 분노 지속 시간
    public float maxVunNoTime = 20;     // 최대 분노 지속 시간
    

    
    [Header("SoundS")]
    public PlayerSound PS;              // PlayerSound Script
    public SinglePlay_GameSoundManager GSM;
    public AudioSource AS_Angly3th;
    


    [Header("About UIManager")]         
    public UIManager uiManager;         // UIManager Script

    public bool Falldownbool;
    public bool VunnoBool;
    public bool Diebool;
    public bool Goalbool;

    public bool DoKey;
    



    #region Player 속성정의
    enum PlayerState
    {
        Idle,           // 대기 상태
        Falldown,       // 시작할때 하늘에서 떨어 지는 상태
        Dash,           // 낙하 직전 "Z"를 누르면 달리는 상태    
        Move,           // 평소 움직임 상태
        Vunno,
        Damage, 
        LowEnergy,      // 에너지를 다 썼을떄 이용하는 상태 
        ElectricShock,  // 감전 당했을때 이용 하는 상태
        Die
    };
    PlayerState State = PlayerState.Falldown; // 플레이어의 상태
    #endregion

    void Start()
    {
        
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();      // PlayerMove의 오브젝트에 있는 UIManager 스크립트 불러오기
        PS = GetComponent<PlayerSound>();           // PlayerMove의 오브젝트에 있는 PlayerSound 스크립트 불러오기
        cc = GetComponent<CharacterController>();   // PlayerMove의 오브젝트에 있는 CharacterController 스크립트 불러오기
        anim = GetComponentInChildren<Animator>();  // PlayerMove의 자식 오브젝트에서 Animator 스크립트 불러오기
        State = PlayerState.Falldown;               // 상태를 Falldown으로 시작한다.
        startSpeed = moveSpeed;                     // 게임 시작시 startSpeed = moveSpeed와 같다.
        curDamageEnergy = 0;                        // 현재 DamageEnergy량을 0으로 시작
        curRunEnergy = 100;                         // 현재 RunEnergy량을 0으로 시작
        uiManager.VunNo.SetActive(false);           // 시작할 때 VunNo키 조합 이미지를 비활성화 한다
        GM.Gameing = false;
        GM.EnterGamebool = false;
    }
    void Update()
    {
        
        uiManager.NowState.text = "State : " + State;                               // 현재 상태를 텍스트로 출력 [uiManager 스크립트에 오브젝트 O]
        uiManager.RunGage.text = "RunGage : " + curRunEnergy;                       // 현재 RunGage를 텍스트로 출력 [uiManager 스크립트에 오브젝트 O]
        uiManager.RunGageFrame.fillAmount = curRunEnergy / maxRunEnergy;            // RunGage의 색채우기 양 지정 [uiManager 스크립트에 오브젝트 O]
        uiManager.DamageGageFrame.fillAmount = curDamageEnergy / maxDamageEnergy;;  // DamageGageFrame의 색채우기 양 지정 [uiManager 스크립트에 오브젝트 O]
        if(GM.Gameing == true && GM.EnterGamebool == true)
        {
            switch (State)
            {
            case PlayerState.Falldown:      // Falldown 상태라면?
                Falldown();                 // Falldown 함수 호출
                break;                      // Switch 조건문 탈출
            
            case PlayerState.Idle:          // Idle 상태라면?
                Idle();                     // Idle 함수 호출
                break;                      // Switch 조건문 탈출  
            
            case PlayerState.Move:          // Move 상태라면?
                Move();                     // Move 함수 호출
                break;                      // Switch 조건문 탈출
            
            case PlayerState.Dash:          // Dash 상태라면?
                Dash();                     // Dash 함수 호출
                break;                      // Switch 조건문 탈출
            case PlayerState.Vunno:     // LowEnergy 상태라면?
                Vunno();                // LowEnergy 함수 호출
                break;                      // Switch 조건문 탈출
            
            case PlayerState.Damage:     // LowEnergy 상태라면?
                Damage();                // LowEnergy 함수 호출
                break;                      // Switch 조건문 탈출

            case PlayerState.LowEnergy:     // LowEnergy 상태라면?
                LowEnergy();                // LowEnergy 함수 호출
                break;                      // Switch 조건문 탈출
            
            case PlayerState.ElectricShock: // ElectricShock 상태라면?
                ElectricShock();            // ElectricShock 함수 호출
                break;                      // Switch 조건문 탈출
            
            case PlayerState.Die:           // Die 상태라면?
                Die();                      // Die 함수 호출
                break;                      // Switch 조건문 탈출
            }
        }
        
        
        print(jumpCount);
        

    }
    

    void Falldown()                                                     // FallDown 함수
    {

        
        yVelocity += gravity * Time.deltaTime;
        Vector3 dir2= Vector3.zero;
        dir2.y = yVelocity;
        
        cc.Move(dir2 * moveSpeed * Time.deltaTime);
        if( cc.isGrounded == false)                                      // 만약 CC가 바닥에 닿지 않았다면?
        {
            GSM.AS_Angly3th.clip = GSM.Angly3thStart[Random.Range(0, 5)];      // Player가 Falldown상태 일때 Angly3thStart 사운드 출력
            GSM.AS_Angly3th.Play();
            if (Input.GetKeyDown(KeyCode.LeftShift))                      // 만약 LeftShift을 눌렀다면?
            {
                PS.AS_State.clip = PS.FallDown[Random.Range(0, 3)];      // PlayerSound의 FallDown 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
                PS.AS_State.Play();                                      // PlayerSound의 FallDown를 AS_State라는 Audio Source에서 나오게 해라 [스피커 역활]
                gravity = -4.8f;                                           // 중력을 -20으로 지정

                print("doKey Down");                             // 키를 눌렀다는 것을 디버킹 출력 해준다
                anim.SetTrigger("FallDown");                       // FallDown 애니메이션 실행
            }
            else                                                        // 그렇지 않다면
            {
                gravity = -0.5f;                                       // 중력을 -9.81로 지정
                print("don't Key Down");                        // 키를 누르지 않음을 디버깅 출력
            }
        }
        if(isCanDash == true)                                           // 만약에 isCanDash(대쉬 가능)이 True라면
        {
            uiManager.CanDashTxt.SetActive(true);                       // CanDashTxt 텍스트를 출력
            print("isgrounded : " + cc.isGrounded);             // 바닥에 붙어 있는지 확인을 위한 출력문
            if(Input.GetKeyDown(KeyCode.Z))                             // isCanDash true 상태인데 만약에 Z를 눌렀다면? 
            {
                
                print("Let's Go : GetKeyDown : Z!");            // Z를 눌렀다는것을 출력
                State = PlayerState.Dash;                               // Z를 누르면 Dash 상태로 이동
                anim.SetBool("Roll",true);                    // Roll 애니메이션 실행 (구르는 애니메이션)
            
            }   
            else if(cc.isGrounded)                                  //  만약에 z를 누르지 못하고 바닥에 붙어 있다면
            {
                DoKey = false;
                State = PlayerState.Damage;
            }
        }
        if (curRunEnergy <= 5)
        {
            Moveto_State_LowEnergy();
        }

    }



    void Idle()
    {
        if (VunnoBool != true)
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                State = PlayerState.Move;
            }
            if (curDamageEnergy >= maxDamageEnergy && curRunEnergy >= maxRunEnergy)
            {
                uiManager.VunNo.SetActive(true);
                uiManager.VunnoGageFrame.SetActive(true);
            }



            if (Input.GetKeyDown(KeyCode.Z) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                VunnoBool = true;
                Invoke("Moveto_State_Vunno", 2f);
                anim.SetTrigger("Angry");
                PS.AS_State.clip = PS.Angry[Random.Range(0, 4)];
                PS.AS_State.Play();
            }

        }
        if (VunnoBool == true)
        {
            State = PlayerState.Vunno;
        }
    }


    void Dash()
    {
        gravity = -9.81f;
        moveSpeed += 5;
        uiManager.GoDashTxt.SetActive(true);                // Dash 상태일때 GoDashTxt 출력 
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Direction", h);
        anim.SetFloat("Speed", v);
        dir = transform.forward * v + transform.right * h ;
        dir.Normalize();
        
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("Walk",true);
            print("State : walk");
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            anim.SetBool("Walk",false);
            State = PlayerState.Idle;
            
        }
        if(curRunEnergy <= maxRunEnergy && Input.GetButton("Run") == false)
        {
            curRunEnergy += 0.05f;
        }
        
        if((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") > 0) && Input.GetButton("Run") && curRunEnergy >= 5)
        {
            anim.SetFloat("RunDirection", h);
            anim.SetFloat("RunSpeed", v);
            moveSpeed += runSpeed;
            anim.SetBool("Run", true);
            anim.SetBool("Walk", false);   
            print("State : Run");
            curRunEnergy -= 0.1f;
        }
        else
        {
            moveSpeed = 5;
            anim.SetBool("Run", false);
        }
        if (curDamageEnergy >= maxDamageEnergy && curRunEnergy >= maxRunEnergy)
        {
            uiManager.VunNo.SetActive(true);
            uiManager.VunnoGageFrame.SetActive(true);
            

            if (Input.GetKeyDown(KeyCode.Z) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                Vunno();
                PS.AS_State.clip = PS.Angry[Random.Range(0, 4)];
                PS.AS_State.Play();
            }
        }
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;
        int layer = 1 << gameObject.layer;
        if (Physics.Raycast(ray, out hitInfo, 1.2f, ~layer) == false)
        {
            anim.SetBool("IsInAir", true);
        }
        if (cc.isGrounded)                              // 만약에 바닥에 붙어있다면
        {
            isJumping = false;                          // isJumping(점프 중인가?)를 false로 지정
            anim.SetBool("IsInAir", false);   // IsInAir(공중에 떠있나?) 애니메이션을 false로 지정
            yVelocity = 0;                              // yVelocity를 0으로 지정
            jumpCount = 0;
        }
        if (isJumping == false && Input.GetButtonDown("Jump"))          // 만약 isJumping(점프 중인가?)가 false이면서 Jump버튼을 눌렀다면
        {
            PS.AS_Move.clip = PS.Jump[Random.Range(0, 2)];              // PlayerSound의 Jump 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
            PS.AS_Move.Play();                                          // PlayerSound의 Jump를 AS_State라는 Audio Source에서 나오게 해라 [스피커 역활]
            anim.SetTrigger("Jump");                               // Jump애니메이션 실행
            print("isgrounded : " + cc.isGrounded);             // 바닥의 붙어 있는지 판별 하기 위해 출력
            yVelocity = jumpPower;                                      // yVelocity에 점프 힘을 대입
            jumpCount++;                                                // jumpCount 증가
            
            if (jumpCount == 2)                                         // 만약 점프 횟수가 2회 라면                                         
            {
                PS.AS_Move.clip = PS.DoubleJump[Random.Range(0, 5)];    // PlayerSound의 DoubleJump 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
                PS.AS_Move.Play();                                      // PlayerSound의 DoubleJump를 AS_State라는 Audio Source에서 나오게 해라 [스피커 역활]
                isJumping = true;                                       // isJumping(점프 중인가?)를 True로 지정 
                anim.SetTrigger("Roll");                           // Roll(구르기)애니메이션 실행
                jumpCount = 0;                                          // 점프 횟수 0으로 초기화
                
            }
        }
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        cc.Move(dir * moveSpeed * Time.deltaTime);
        Invoke("Moveto_State_Move", 3f);
       
    }
    void Move()
    {
        gravity = -9.81f;
        anim.SetBool("LowEnergy",false);
        uiManager.VunNo.SetActive(false);
        uiManager.VunnoGageFrame.SetActive(false);
        uiManager.GoDashTxt.SetActive(false);               // GoDashTxt 비활성화
        uiManager.CanDashTxt.SetActive(false);              // CanDashTxt 비활성화
        uiManager.DamageTxt.SetActive(false);               // DamageTxt 비활성화

        if (DoKey == true) // Dokey의 기능은 키 입력이 가능 하도록
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            anim.SetFloat("Direction", h);
            anim.SetFloat("Speed", v);
            Vector3 dir = new Vector3(h, 0, v);
            // v = v0 + at
            yVelocity += gravity * Time.deltaTime;

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                anim.SetBool("Walk",true);
                print("State : walk");
            }
            else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                anim.SetBool("Walk",false);
                State = PlayerState.Idle;
                
            }
            if(curRunEnergy <= maxRunEnergy && Input.GetButton("Run") == false)
            {
                curRunEnergy += 0.05f;
            }
            
            if((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && Input.GetButton("Run") && curRunEnergy >= 5)
            {
                anim.SetFloat("RunDirection", h);
                anim.SetFloat("RunSpeed", v);
                moveSpeed = runSpeed;
                anim.SetBool("Run", true);
                anim.SetBool("Walk", false);   
                print("State : Run");
                curRunEnergy -= 0.1f;
            }
            else
            {
                moveSpeed = startSpeed;
                anim.SetBool("Run", false);
            }
            

            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hitInfo;
            int layer = 1 << gameObject.layer;
            if (Physics.Raycast(ray, out hitInfo, 1.2f, ~layer) == false)
            {
                anim.SetBool("IsInAir", true);
            }
            if (cc.isGrounded)                              // 만약에 바닥에 붙어있다면
            {
                //isJumping = false;                          // isJumping(점프 중인가?)를 false로 지정
                anim.SetBool("IsInAir", false);   // IsInAir(공중에 떠있나?) 애니메이션을 false로 지정
                yVelocity = 0;                              // yVelocity를 0으로 지정
                jumpCount = 0;
            }
            if (jumpCount < maxjumpCount && Input.GetButtonDown("Jump"))          // 만약 isJumping(점프 중인가?)가 false이면서 Jump버튼을 눌렀다면
            {
                PS.AS_Move.clip = PS.Jump[Random.Range(0, 2)];              // PlayerSound의 Jump 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
                PS.AS_Move.Play();                                          // PlayerSound의 Jump를 AS_State라는 Audio Source에서 나오게 해라 [스피커 역활]
                anim.SetTrigger("Jump");                               // Jump애니메이션 실행
                print("isgrounded : " + cc.isGrounded);             // 바닥의 붙어 있는지 판별 하기 위해 출력
                yVelocity = jumpPower;                                      // yVelocity에 점프 힘을 대입
                jumpCount++;                                                // jumpCount 증가
                
                if (jumpCount == 2)                                         // 만약 점프 횟수가 2회 라면                                         
                {
                    PS.AS_Move.clip = PS.DoubleJump[Random.Range(0, 5)];    // PlayerSound의 DoubleJump 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
                    PS.AS_Move.Play();                                      // PlayerSound의 DoubleJump를 AS_State라는 Audio Source에서 나오게 해라 [스피커 역활]
                    //isJumping = true;                                       // isJumping(점프 중인가?)를 True로 지정 
                    anim.SetTrigger("Roll");                           // Roll(구르기)애니메이션 실행   
                }
            }
            //yVelocity += gravity * Time.deltaTime;
            dir.y = yVelocity;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        if(curDamageEnergy >= maxDamageEnergy && curRunEnergy >= maxRunEnergy)
        {
            uiManager.VunNo.SetActive(true);
            uiManager.VunnoGageFrame.SetActive(true);
                
            if(Input.GetKeyDown(KeyCode.Z) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                VunnoBool = true;
                Invoke("Moveto_State_Vunno",2f);
                anim.SetTrigger("Angry");
                PS.AS_State.clip = PS.Angry[Random.Range(0, 4)];
                PS.AS_State.Play();
            }
        }

        if (curRunEnergy <= 5)
        {
            DoKey = false;
            State = PlayerState.LowEnergy;
        }

        
    }

    void Vunno()
    {
        VunnoBool = true;
        anim.SetBool("Run", true);
        gravity = -9.81f;
        anim.SetBool("LowEnergy",false);
        uiManager.GoDashTxt.SetActive(false);               // GoDashTxt 비활성화
        uiManager.CanDashTxt.SetActive(false);              // CanDashTxt 비활성화
        uiManager.DamageTxt.SetActive(false);               // DamageTxt 비활성화
        Invoke("Moveto_State_Move",10f);
        if (DoKey == true)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            moveSpeed = 16;
            anim.SetFloat("Direction", h);
            anim.SetFloat("Speed", v);
            dir = transform.forward * v + transform.right * h ;
            dir.Normalize();

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                anim.SetBool("Run",true);
                print("State : walk");
            }
        

            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hitInfo;
            int layer = 1 << gameObject.layer;
            if (Physics.Raycast(ray, out hitInfo, 1.2f, ~layer) == false)
            {
                anim.SetBool("IsInAir", true);
            }
            if (cc.isGrounded)                              // 만약에 바닥에 붙어있다면
            {
                isJumping = false;                          // isJumping(점프 중인가?)를 false로 지정
                anim.SetBool("IsInAir", false);   // IsInAir(공중에 떠있나?) 애니메이션을 false로 지정
                yVelocity = 0;                              // yVelocity를 0으로 지정
                jumpCount = 0;
            }
            if (isJumping == false && Input.GetButtonDown("Jump"))          // 만약 isJumping(점프 중인가?)가 false이면서 Jump버튼을 눌렀다면
            {
                PS.AS_Move.clip = PS.Jump[Random.Range(0, 2)];              // PlayerSound의 Jump 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
                PS.AS_Move.Play();                                          // PlayerSound의 Jump를 AS_State라는 Audio Source에서 나오게 해라 [스피커 역활]
                anim.SetTrigger("Jump");                               // Jump애니메이션 실행
                print("isgrounded : " + cc.isGrounded);             // 바닥의 붙어 있는지 판별 하기 위해 출력
                yVelocity = jumpPower;                                      // yVelocity에 점프 힘을 대입
                jumpCount++;                                                // jumpCount 증가
                
                if (jumpCount == 2)                                         // 만약 점프 횟수가 2회 라면                                         
                {
                    PS.AS_Move.clip = PS.DoubleJump[Random.Range(0, 5)];    // PlayerSound의 DoubleJump 사운드 배열에서 랜덤으로 클립에 넣어라  [CD 역활]
                    PS.AS_Move.Play();                                      // PlayerSound의 DoubleJump를 AS_State라는 Audio Source에서 나오게 해라 [스피커 역활]
                    isJumping = true;                                       // isJumping(점프 중인가?)를 True로 지정 
                    anim.SetTrigger("Roll");                           // Roll(구르기)애니메이션 실행
                    jumpCount = 0;                                          // 점프 횟수 0으로 초기화
                    
                }
            }
            yVelocity += gravity * Time.deltaTime;
            dir.y = yVelocity;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            curRunEnergy = 100;
            curDamageEnergy = 0;
        }
        
    }
    
    

    public void OnDamagedEnergy()
    {
        if (curDamageEnergy >= maxDamageEnergy)     //maxDamageEnergy보다 curDamageEnergy가 더 큰 경우
        {
            print("?? ??? ? ?"); 
        }

    }


    void Damage()
    {
        DoKey = false;
        anim.SetTrigger("Damage");                    // Damage 애니메이션 실행
        PS.AS_Move.clip = PS.Damage[Random.Range(0, 3)];
        PS.AS_Move.Play();
        uiManager.DamageTxt.SetActive(true);                // 현재 데미지를 입었다는 텍스트를 출력
        moveSpeed = 0;                                      // 이동속도는 0으로 지정
        yVelocity = 0;
        Invoke("Moveto_State_Move", 3f);
        
    }
    void LowEnergy()
    {
        
        PS.AS_Move.clip = PS.LowEnergy[Random.Range(0, 3)];
        PS.AS_Move.Play();
        anim.SetBool("LowEnergy",true);
        dir.y = yVelocity;
        Invoke("Moveto_State_Move", 3);
        
    }
    void ElectricShock()
    {
        DoKey = false;
        PS.AS_State.clip = PS.ElectricShock[Random.Range(0, 3)];
        PS.AS_State.Play();
        anim.SetTrigger("electric shock");
        
        Invoke("Moveto_State_Move", 2);
        
    }

    void Die()
    {
        DoKey = false;
        Diebool = true;
        anim.SetTrigger("Die");
        GSM.AS_Angly3th.clip = GSM.Angly3thFailed[Random.Range(0, 3)];      // Player가 Falldown상태 일때 Angly3thStart 사운드 출력
        GSM.AS_Angly3th.Play();
        GM.FailedPopup.SetActive(true);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("XBox"))
        {
            if (VunnoBool != true)
            {
                PS.AS_State.clip = PS.Damage[Random.Range(0, 3)];
                PS.AS_State.Play();
                anim.SetTrigger("XBox");
                curDamageEnergy += 1f;
                print(curDamageEnergy);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("BlueElec"))
        {
            if (VunnoBool == false)
            {
                DoKey = false;
                State = PlayerState.ElectricShock;
                curDamageEnergy += 1f;
                PS.AS_Move.clip = PS.Damage[Random.Range(0, 6)];
                PS.AS_Move.Play();
            }

        }
        if(other.gameObject.CompareTag("RedElec"))
        {
            
            PS.AS_State.clip = PS.ElectricShock[Random.Range(0, 3)];
            PS.AS_State.Play();
            Moveto_State_Die();
            
        }
        if (other.gameObject.CompareTag("Goal"))
        {
            PS.AS_State.clip = PS.Victory[Random.Range(0, 3)];
            PS.AS_State.Play();
            
            GSM.AS_Angly3th.clip = GSM.Angly3thGoal[Random.Range(0, 1)];      // Player가 Falldown상태   일때 Angly3thStart 사운드 출력
            GSM.AS_Angly3th.Play();
            GM.Gameing = false;
            if(GM.Gameing == false)
            {
                GM.goalPopup.SetActive(true);
                Score = string.Format("{0:D2} :{1:D2}", _Min, (int)_Sec);
                GM.ScoreTxt.text = "기록 : " + Score;
            }
        }
    }
    void Moveto_State_Move()
    {
        VunnoBool = false;
        DoKey = true;
        State = PlayerState.Move;
    }
    void Moveto_State_Vunno()
    {
        VunnoBool = true;
        DoKey = true;
        State = PlayerState.Vunno;
    }
    void Moveto_State_LowEnergy()
    {
        State = PlayerState.LowEnergy;
    }
    void Moveto_State_Die()
    {
        State = PlayerState.Die;
    }
    void Timer()
    {
        if (GM.Gameing == true)
        {
            _Sec += Time.deltaTime;
            _TimerText.text = "Time : 00 :" + string.Format("{0:D2} :{1:D2}", _Min, (int)_Sec);
            if ((int)_Sec > 59)
            {
                _Sec = 0;
                _Min++;
            }
        }

    }

}
 