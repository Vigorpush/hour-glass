using System;



[Serializable]
public class Range
{

	protected int max; 
	protected int min;

	public Range (int low, int high)
	{
		max = high;
		min = low; 
	}

	public int getMax (){
		return max;
	}

	public int getMin (){
		return min;
	}

	public int getRandRange(){
		return UnityEngine.Random.Range (min, max);
	}
			
}


