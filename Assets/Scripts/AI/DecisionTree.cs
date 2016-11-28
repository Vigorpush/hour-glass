using UnityEngine;
using System.Collections;

public class DecisionTree {

    public delegate bool Decision();
    public delegate void Action();
    Decision myDecision;
    Action myAction;
    public DecisionTree left;
    public DecisionTree right;

    public DecisionTree()
    {
        left = null;
        right = null;
        myDecision = null;
        myAction = null;
    }

    public DecisionTree getLeft()
    {
        return left;
    }
    public DecisionTree getRight()
    {
        return right;
    }
    public void setLeft(DecisionTree leftTree)
    {
         left=leftTree;
    }
    public void setRight(DecisionTree rightTree)
    {
        right=rightTree;
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
        if(node.getRight()==null && node.getLeft()==null)
        {
            Action doAction = node.getAction();
            doAction();
            Debug.Log("Decided to "+doAction.Method);
            return;
        }
        //else not a leaf, make decision and recursive call
        Decision makeDecision = node.getDecision();
       // Debug.Log("Thinking about " + makeDecision.Method);
        if (makeDecision() == true)         // if decision true go left
        {
            Search(node.getLeft());
        } else if (makeDecision() == false)  //if decision false to right
        {
            Search(node.getRight());
        }
    }

}
