using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	bool _leftDown;
	public bool leftDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.LeftArrow))
				return true;
			if(_leftDown)
			{
				_leftDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{_leftDown = value;}
	}

	bool _rightDown;
	public bool rightDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.RightArrow))
				return true;
			if(_rightDown)
			{
				_rightDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{_rightDown = value;}
	}

	bool _downDown;
	public bool downDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.DownArrow))
				return true;
			if(_downDown)
			{
				_downDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{_downDown = value;}
	}

	bool _upDown;
	public bool upDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.UpArrow))
				return true;
			if(_upDown)
			{
				_upDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{_upDown = value;}
	}

	bool _ADown;
	public bool Adown
	{
		get{
			if(Input.GetKeyDown(KeyCode.X))
				return true;
			if(_ADown)
			{
				_ADown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{_ADown = value;}
	}

	bool _BDown;
	public bool Bdown
	{
		get{
			if(Input.GetKeyDown(KeyCode.C))
				return true;
			if(_BDown)
			{
				_BDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{_BDown = value;}
	}

	bool _XDown;
	public bool Xdown
	{
		get{
			if(Input.GetKeyDown(KeyCode.V))
				return true;
			if(_XDown)
			{
				_XDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{_XDown = value;}
	}

	public IEnumerator Wait(float duration)
	{
		print(Time.time);
		yield return new WaitForSeconds (duration);
		print(Time.time);
	}

}
