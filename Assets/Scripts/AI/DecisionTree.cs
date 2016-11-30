using UnityEngine;
using System.Collections;

public class DecisionTree {

	public delegate int Decision();
    public delegate void Action();

    Decision myDecision;
    Action myAction;
	protected ArrayList children = new ArrayList();
 

    public DecisionTree()
    {
		
        myDecision = null;
        myAction = null;
    }

 

    public void addChild(DecisionTree child)
    {
		children.Add (child);
    }
    public Decision getDecision(){
		if(this.myDecision == null){
			Debug.Log("no decision!!!!!");
		}
        return myDecision;
    }
    public Action getAction()
    {
        return myAction;
    }
    public void setDecision(Decision nodeDecision)
    {
        myDecision = nodeDecision;
    }
    public void setAction(Action nodeAction)
    {
        myAction = nodeAction;
    }

    public void Search(DecisionTree node)
    {
        //if at a leaf, no decision, just do action
		if(node.children.Count == 0)
        {
            Action doAction = node.getAction();
            doAction();

            Debug.Log("Decided to "+doAction.Method);
            return;
		
        }
        //else not a leaf, make decision and recursive call

		Decision d =node.getDecision();
		int result = d (); 
		Debug.Log("d is"+ d);



       // Debug.Log("Thinking about " + makeDecision.Method);

		Search((DecisionTree)children[result]);
       
    }

}
