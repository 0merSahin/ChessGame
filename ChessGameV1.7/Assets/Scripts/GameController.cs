using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BoardService boardService;
    public SoldierService soldierService;
    public MoveControl moveControl;
    public CanvasService canvasService;
    public List<Soldier> soldierList;
    
    
    public GameObject kingWhite;
    public GameObject queenWhite;
    public GameObject castleWhite;
    public GameObject bishopWhite;
    public GameObject knightWhite;
    public GameObject pawnWhite;
    public GameObject kingBlack;
    public GameObject queenBlack;
    public GameObject castleBlack;
    public GameObject bishopBlack;
    public GameObject knightBlack;
    public GameObject pawnBlack;

    private Collider2D hitCollider;
    public Collider2D selectCollider;

    public bool selectSoldierKey = true;
    public bool moveSoldierKey = true;
    public bool panelSelectKey = false;
    public int defeatedWhiteSoldierCount;
    public int defeatedBlackSoldierCount;
    public Transform lastPawnTransform;


    void Start()
    {
        boardService = new BoardService(this);
        moveControl = new MoveControl(this);
        canvasService = new CanvasService(this);
        soldierList = new List<Soldier>();

        soldierService = new SoldierService(this, boardService);
        soldierService.CreateSoldiers();

        defeatedWhiteSoldierCount = 0;
        defeatedBlackSoldierCount = 23;
    }


    void Update()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                hitCollider = hit.collider;
            }
        }

        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            hitCollider = hit.collider;
        }

        
        if (!panelSelectKey && hitCollider != null && hitCollider.tag == "Board")
        {
            if (selectSoldierKey)
            {
                selectSoldierKey = false;
                selectCollider = hitCollider;
                moveControl.SelectSoldier(hitCollider);
            }
            else if (moveSoldierKey)
            {
                moveSoldierKey = false;
                if (hitCollider == selectCollider) // seçim iptal edilecek:
                {
                    moveSoldierKey = true;
                    selectSoldierKey = true;
                    selectCollider = null;
                    boardService.ResetAllSquareColor();
                    soldierService.ResetSoldierMoveLists(hitCollider);
                }

                else if (moveControl.MoveSoldier(hitCollider, selectCollider, this)) // Soldier hareket ettirildi:
                {
                    if (!moveControl.isRokMove)
                        soldierService.ResetSoldierMoveLists(hitCollider);
                    else
                        moveControl.isRokMove = false;
                    
                    // Oyun sırası değiştirme:
                    if (moveControl.orderOfMoves) moveControl.orderOfMoves = false;
                    else moveControl.orderOfMoves = true;
                    canvasService.UpdateText();
                }
                else // Soldier hareket ettirilmedi:
                    soldierService.ResetSoldierMoveLists(selectCollider);
                boardService.ResetAllSquareColor();
                selectSoldierKey = true;
                moveSoldierKey = true;
            }
            hitCollider = null;
        }

        if (panelSelectKey && hitCollider != null && hitCollider.tag == "SoldierPanel")
        {
            PanelService.PanelOperation(hitCollider, this);
        }
    }
}
