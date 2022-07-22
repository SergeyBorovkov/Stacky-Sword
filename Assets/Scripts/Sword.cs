using UnityEngine;

public class Sword : MonoBehaviour
{    
    [SerializeField] private BladePiece _bladePiecePrefab;
    [SerializeField] private Transform _bladesParent;
    [SerializeField] private Handle _handle;    
    [SerializeField] private int _maxBladePieces;
    [SerializeField] private int _startBladePieces;
    [SerializeField] private SwordMover _mover;
            
    private bool _isStopped;
    private float _bladePieceHeight = 2.55f;

    private void OnEnable()
    {
        _handle.IsHitObstacle += DestroyAllBlades;
    }

    private void OnDisable()
    {
        _handle.IsHitObstacle -= DestroyAllBlades;
    }

    private void Start()
    {
        for (int i = 0; i < _startBladePieces; i++)
        {
            CreateBladePiece();
        }        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isStopped == false)
        {
            AddBladePiece();
        }        
    }

    public void DestroyAllBlades()
    {
        _mover.Stop();

        _isStopped = true;

        var blades = GetBladesArray();

        foreach (var blade in blades)
        {
            if (blade.IsConnected)
                blade.DestroyConnectedBlade();
        }
    }

    private void CreateBladePiece()
    {       
        var newBladePosition = new Vector3 (_bladesParent.position.x, _bladesParent.position.y + _bladePieceHeight, _bladesParent.position.z);

        _handle.MoveUp(_bladePieceHeight);
                
        MoveConnectedBladesDown();        
        
        var newBlade = Instantiate(_bladePiecePrefab, newBladePosition, Quaternion.identity, _bladesParent);

        newBlade.StartChangeScale();        
    }
        
    private void AddBladePiece()
    {
        if (_bladesParent.childCount < _maxBladePieces)       
            CreateBladePiece();        
    }
    
    private void MoveConnectedBladesDown()
    {
        var blades = GetBladesArray();

        foreach (var blade in blades)
        {
            if (blade.IsConnected)
                blade.MoveDown(_bladePieceHeight);
        }
    }    

    private BladePiece[] GetBladesArray()
    {
        return _bladesParent.GetComponentsInChildren<BladePiece>();       
    }
}