Shop&Wallet

Unity Version: 2020.3.25f1

Installed Packages:
- None

Installed Assets:
- TMP

This project is a basic template used to create a shop / storefront for unity projects.
It is still lacking some flexibility and functionality and is mostly focused on the backend logic.

How a storefront is accessed by a wallet is determined by your project/game design and and such
only a very basic example with the most straight forward approach is included in the project.

How to:
1. Attach Wallet.cs to a gameobject. 
2. Attach Storefront.cs to an object. 
3. Create a new Catalog for the store through <ShopWallet> scriptable object menu in project assets.
4. Create new Merchandise for the store though <ShopWallet> sriptable object menu in project assets.
5. Assign Merchandise to the created Catalog.
6. Assign Catalog to Storefront via inspector reference. 
(Optionally also assign TransactionProcessor which is already created)
7. Design and create the front end storefront and how it is accessible for your project.
(Open BobsBuildingShop example for details)
