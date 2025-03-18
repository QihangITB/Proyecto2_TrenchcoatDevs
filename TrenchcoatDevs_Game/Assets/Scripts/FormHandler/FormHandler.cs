using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * This abstract class should be used by any handler that does changes to data of other GameObjects
 * whenever a the programer feels like its time to Update a gameObject data with the form elements or
 * do actions with GameObjects.
 * </summary>
 */
public abstract class FormHandler : MonoBehaviour
{
    /**
     * <summary>
     * This method should be called whenever you want to update wathever element from another GameObject the form changes
     * </summary>
     **/
    public abstract void ChangeData();
    /**
     * <summary>
     * This method should be called when the child objects need to verify that all data is correctly inputed.
     * </summary>
     **/
    protected virtual bool VerifyData()
    {
        //The abstract class has nothing to verify but childs of it may need to.
        return true;
    }
    /**
     * <summary>
     * This method should be called when you want another GameObject to perform some kind of action.
     * For example start a server.
     * </summary>
     **/
    public abstract void DoAction();
}
