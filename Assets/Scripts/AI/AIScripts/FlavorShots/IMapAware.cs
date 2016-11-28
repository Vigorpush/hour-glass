using UnityEngine;
using System.Collections;

public interface MapAware {
	FearMap mostRecentMap{ get; set; }

	void RequestMapFromBM();
	void UpdateMap();



}
