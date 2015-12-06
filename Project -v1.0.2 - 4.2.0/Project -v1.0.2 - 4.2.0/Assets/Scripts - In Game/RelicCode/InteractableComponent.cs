using UnityEngine;
using System.Collections;

public class InteractableComponent : MonoBehaviour
{
	/*
    public bool IsInteractable { get; set; }
//    private bool CollisionIsTrigger;//Just have the Interactable object ask this InteractableComponent whether CollisionIsTrigger
//    private bool repeatable = true;
//    private bool triggeredOnce = false;
	public string ControllerHelperText = "Press B to Dash";
	public string KeyboardHelperText = "Press K to Dash";
    public bool forceShowHelperText = false;
//    private bool hasDirecetionIndicator = false;
	private bool _showNotification;
    private bool _showLargeNotification;
    private float _elapsedTime = 0f;
	
	private bool _hasLargeNotification = true;
	private bool _hasNotification = true;
	private float _largeNotifyTimer;
	private float _notifyTimer;
    private Shader outlineShader;
    public Color shaderColor = Color.white;
    public Color outlineColor = new Color (206f/256f, 206f/256f, 206f/256f);
    public float outlineWidth = 0.002f;
	private float maxOutlineWidth;
    public Color insetColor = Color.white;
	private float insetAlpha = 0f;
	private MeshRenderer[] meshRenderers;
	private MeshRenderer meshRenderer;

    public float dashCollisionSize = 3f;
    private bool showingHelperText = false;

	private bool isPillar;
	private bool _insetAlphaChanged;
	private bool _outlineChanged;

    void Start()
    {
		IsInteractable = true;
		_insetAlphaChanged = true;
		_outlineChanged = true;
		maxOutlineWidth = outlineWidth;
		outlineWidth = 0f;
		outlineShader = Shader.Find("Custom/Outline_NoTrans");
		isPillar = gameObject.GetComponent<Pillar> () != null ? true : false;
		if (isPillar)
		{
			meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer> ();	
			meshRenderer.material.shader = outlineShader;
			meshRenderer.material.SetFloat("_Outline", outlineWidth);
			meshRenderer.material.SetFloat("_InsetAlpha", 0f);
			meshRenderer.material.SetColor("_Color", shaderColor);
			meshRenderer.material.SetColor("_OutlineColor", outlineColor);
			meshRenderer.material.SetColor("_InsetColor", insetColor);

		}
		else
		{
			meshRenderers = this.gameObject.GetComponentsInChildren<MeshRenderer> ();		
			foreach(MeshRenderer meshRenderer in meshRenderers){
				meshRenderer.material.shader = outlineShader;
				meshRenderer.material.SetFloat("_Outline", outlineWidth);
				meshRenderer.material.SetFloat("_InsetAlpha", 0f);
				meshRenderer.material.SetColor("_Color", shaderColor);
				meshRenderer.material.SetColor("_OutlineColor", outlineColor);
				meshRenderer.material.SetColor("_InsetColor", insetColor);
			}
		}

        //if (_hasLargeNotification)
        //{		


		dashCollisionSize = dashCollisionSize * gameObject.transform.localScale.z;
        //}
    }

    void Update()
	{
		_elapsedTime += Time.deltaTime;
		if (_largeNotifyTimer > 0){
			_largeNotifyTimer -= Time.deltaTime;
		}
		if (_notifyTimer > 0){
			_notifyTimer -= Time.deltaTime;
		}
        if (_largeNotifyTimer < 0){			
			_showLargeNotification = false;
//			meshRenderer.material.shader = initialShader;
		}
		if (_largeNotifyTimer < 0 || _notifyTimer < 0){
            showingHelperText = false;
			if (insetAlpha > 0) {
				_insetAlphaChanged = true;
                insetAlpha -= Time.deltaTime * 5;
                if (insetAlpha < 0)
                {
                    insetAlpha = 0;
                }
	        }
			if (outlineWidth > 0){
				_outlineChanged = true;
				outlineWidth -= Time.deltaTime;
                if (outlineWidth < 0){
                    outlineWidth = 0;
                }
			}
		}
		if (_largeNotifyTimer > 0 || _notifyTimer > 0){
			if (insetAlpha < 1){
				_insetAlphaChanged = true;
                insetAlpha += Time.deltaTime * 5;
                if (insetAlpha > 1)
                {
                    insetAlpha = 1;
                }
			}
			if (outlineWidth < maxOutlineWidth){
				_outlineChanged = true;
                outlineWidth += Time.deltaTime;
                if (outlineWidth > maxOutlineWidth)
                {
                    outlineWidth = maxOutlineWidth;
                }
			}
		}
		if (isPillar) 
		{
			if(_outlineChanged)
				meshRenderer.material.SetFloat("_Outline", outlineWidth);
			if(_insetAlphaChanged)
				meshRenderer.material.SetFloat("_InsetAlpha", insetAlpha);
		}
		else
		{
			foreach(MeshRenderer meshRenderer in meshRenderers){
				if(_outlineChanged)
					meshRenderer.material.SetFloat("_Outline", outlineWidth);
				if(_insetAlphaChanged)
					meshRenderer.material.SetFloat("_InsetAlpha", insetAlpha);
			}
		}
		_outlineChanged = false;
		_insetAlphaChanged = false;
    }

    public delegate void NotifyAction(InteractableNotifyEventData data);
    public delegate void LargeNotifyAction(InteractableLargeNotifyEventData data);
    public delegate void InteractAction(InteractableInteractEventData data);
    public delegate void DashedAction(InteractableInteractEventData data);

    public event NotifyAction OnNotify;
    public event LargeNotifyAction OnLargeNotify;
    public event InteractAction OnInteract;
    public event DashedAction OnDashed;

    public virtual void NotifyProximity(InteractableNotifyEventData data)
	{
        if (IsInteractable)
        {
            if (OnNotify != null)
            {
                OnNotify(data);
				if (_hasNotification)
				{
					ShowNotification ();
					_notifyTimer = 0.1f;
					_showNotification = true;
					
				}
            }
        }
    }

    public virtual void NotifyLargeProximity(InteractableLargeNotifyEventData data)
	{
        if (IsInteractable)
        {
            if (OnLargeNotify != null)
            {
                OnLargeNotify(data);
	            if (_hasLargeNotification)
	            {
					ShowNotification ();
	                _largeNotifyTimer = 0.1f;
	                _showLargeNotification = true;

				}
			}
        }
    }

    public virtual void NotifyInteraction(InteractableInteractEventData data)
	{
        if (IsInteractable)
        {
            if (OnInteract != null)
            {
                OnInteract(data);
            }
        }
    }

    public virtual void NotifyDashed(InteractableInteractEventData data)
	{
        if (IsInteractable)
        {
            if (OnDashed != null)
            {
                OnDashed(data);
            }
        }
    }

	public void disablenotification(){
		forceShowHelperText = false;

	}

	private void ShowNotification(){

		if (forceShowHelperText) {
						if (shouldShowHelperText ()) {
								if (!showingHelperText) {
										string HelperText;
										if (PlayerUtils.CurrentInputType == PlayerUtils.InputType.Controller) {
												HelperText = ControllerHelperText;
										} else {
												HelperText = KeyboardHelperText;
										}
										GameUI.DisplayInstructionTextArea (HelperText, 0.1f);
										showingHelperText = true;
								}
								GameUI.UpdateInstructionDuration (0.1f);
						}
				}
	}
   
    public bool getShowLargeNotification()
    {
        return _showLargeNotification;
    }
	public bool getShowNotification()
	{
		return _showNotification;
	}

//    void OnTriggerEnter(Collider other)
//	{
//        if (CollisionIsTrigger && (repeatable || !triggeredOnce))
//        {
//            triggeredOnce = true;
//            OnInteract(null);
//        }
//    }

    private bool shouldShowHelperText()
    {
        GameObject source = this.gameObject;
        bool result = true;
        GameObject[] inventories = GameObject.FindGameObjectsWithTag("Inventory");
        if (inventories.Length > 0)
        {
//            string type = gameObject.name;
            StatTracker st = inventories[0].GetComponent<StatTracker>();
            result = st.shouldShowHelperText(source);
        }
        return result;
    }*/
}
