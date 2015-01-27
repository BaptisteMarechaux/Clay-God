using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	bool _leftDown;
	public bool leftDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.LeftArrow))
				return true;
			return _leftDown;
		}
		set{_leftDown = value;}
	}

	bool _rightDown;
	public bool rightDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.RightArrow))
				return true;
			return _rightDown;
		}
		set{_rightDown = value;}
	}

	bool _downDown;
	public bool downDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.DownArrow))
				return true;
			return _downDown;
		}
		set{_downDown = value;}
	}

	bool _upDown;
	public bool upDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.UpArrow))
				return true;
			return _upDown;
		}
		set{_upDown = value;}
	}

	bool _ADown;
	public bool Adown
	{
		get{
			if(Input.GetKeyDown(KeyCode.X))
				return true;
			return _ADown;
		}
		set{_ADown = value;}
	}

	bool _BDown;
	public bool Bdown
	{
		get{
			if(Input.GetKeyDown(KeyCode.C))
				return true;
			return _BDown;
		}
		set{_BDown = value;}
	}

	bool _XDown;
	public bool Xdown
	{
		get{
			if(Input.GetKeyDown(KeyCode.V))
				return true;
			return _XDown;
		}
		set{_XDown = value;}
	}

}
