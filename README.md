Shop&Wallet

Unity Version: 2020.3.25f1

Installed Packages:

None

Installed Assets:

TMP

This project is a basic template used to create a shop / storefront for unity projects.
It is still lacking some flexibility and functionality and is mostly focused on the backend logic.

How a storefront is accessed by a wallet is determined by your project/game design and and such
only a very basic example with the most straight forward approach is included in the project.

How to:

-Attach Wallet.cs to a gameobject. 

-Attach Storefront.cs to an object. 

-Create a new Catalog for the store through <ShopWallet> scriptable object menu in project assets.

-Create new Merchandise for the store though <ShopWallet> sriptable object menu in project assets.

-Assign Merchandise to the created Catalog.

-Assign Catalog to Storefront via inspector reference. 
(Optionally also assign TransactionProcessor which is already created)

-Design and create the front end storefront and how it is accessible for your project.
(Open BobsBuildingShop example for details)