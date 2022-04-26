Code snips

Linq Expressions and Ternary operations
shoppingcart, ecommerce fragments
	{
		if (_aciContext.ACIValueOrders.Where(x => x.OrderID == shoppingCart.OrderID).Any())
		{
			return shoppingCart.OrderID;
		}
		shoppingCart.OrderID = _aciContext.ACIValueOrders.Select(x => x.OrderID).Max() + 1;
		_aciContext.ACIValueOrders.Add(shoppingCart);
	}

Awaits that occur below this code cannot be inside a lock.

            bool continueProcessing;
            lock (_lockerProcessing)
            {
                somethingGeneric.Uid = uidString;
                continueProcessing = (somethingGeneric.OrderState != somethingGeneric.orderStates.inProcess);
            }
            if (continueProcessing && ....
{...