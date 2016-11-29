using UnityEngine;
using System.Collections;

public class DecisionTree {

	public delegate int Decision();
    public delegate void Action();

    Decision myDecision;
    Action myAction;
	ArrayList children;
    public DecisionTree left;
    public DecisionTree right;

    public DecisionTree()
    {
        left = null;
        right = null;
        myDecision = null;
        myAction = null;
    }

  
	public DecisionTree getChild(int childIndex)
    {
        return right;
    }

    public void addChild(DecisionTree child)
    {
		children.Add (child);
    }
    public Decision getDecision(){
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
		if(children.Count == 0)
        {
            Action doAction = node.getAction();
            doAction();
            Debug.Log("Decided to "+doAction.Method);
            return;
        }
        //else not a leaf, make decision and recursive call
        Decision makeDecision = node.getDecision();
		Search(node.getDecision());
     
    }

}
