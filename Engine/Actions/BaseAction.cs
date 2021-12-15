using System;
using Engine.Models;

namespace Engine.Actions {
    public abstract class BaseAction {                  //Abstract means it can't be instantiated - just it's children
        protected readonly GameItem _itemInUse;

        public event EventHandler<string> OnActionPerformed;

        protected BaseAction(GameItem itemInUse) {      //This is a constractor that can be called by children of this class 
            _itemInUse = itemInUse;
        }

        protected void ReportResult(string result) {    //Function that raises the event notification - if anything subscribed to the OnActionPerformed event
            OnActionPerformed?.Invoke(this, result);    //Protected means it can be only called by this class elements or it's child
        }
    }
}
