using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{


	private RuntimePlatform _platform = Application.platform;

	private Animator _animator;
	

	void Update () 
	{
		if (_platform == RuntimePlatform.Android || _platform == RuntimePlatform.IPhonePlayer)
		{
			if (Input.touchCount > 0)
			{
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					CheckTouch(Input.GetTouch(0).position);
				}
			}
		}
		else
		{
			if (_platform == RuntimePlatform.WindowsEditor)
			{
				if (Input.GetMouseButtonDown(0))
				{
					CheckTouch(Input.mousePosition);
				}
			}
		}
	}


	
	private void CheckTouch(Vector3 position)
	{
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit raycastHit;
		if (Physics.Raycast(ray, out raycastHit))
		{
			Debug.Log("Hit");
			Debug.Log(raycastHit.transform.gameObject.name);
			if (raycastHit.transform.gameObject.GetComponentInParent<Animator>() != null)
			{
				_animator = raycastHit.transform.gameObject.GetComponentInParent<Animator>();
				Debug.Log("start");

				GetComponent<PhotonView>().RPC("PerformAction",PhotonTargets.All);
				
//				raycastHit.transform.gameObject.GetComponentInParent<Animator>().SetTrigger("action");
			}
		}

	}
	
	
	[PunRPC]
	private void PerformAction()
	{
		_animator.SetTrigger("action");
	}
	
}
