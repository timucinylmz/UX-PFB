# UI for allowing users to submit PayForBlob Transactions on Celestia's Blockspace Race Testnet

## Explanation

This web app is developed by me for Blockspace Race testnet's bonus task which is organised by Celestia. You can reach the app via your browser which has address http://128.140.9.56/  

![celestia reklam](https://github.com/timucinylmz/UX-PFB/assets/50196616/ac281673-7f78-41c6-9d64-ad318b6d1284)

## Description

Create a UI for allowing users to submit PayForBlob Transactions. You can check out the Node tutorial (https://docs.celestia.org/developers/node-tutorial/). It shows you how you can call the API in order to submit a PFB transaction (https://docs.celestia.org/developers/node-tutorial/#submit-a-pfb-transaction) , and how to retrieve the data (https://docs.celestia.org/developers/node-tutorial/#get-namespaced-shares-by-block-height) by block height and namespace.  

![devs](https://github.com/timucinylmz/UX-PFB/assets/50196616/2516216f-d6b4-4449-9cfe-d8768669e6c4)


## Instructions for running the app on your own device

*Requirements:*  
	-Node.js 18+    
	-dotnet sdk 6  
	-quasar/cli with:  
		    `npm i -g @quasar/cli`


*For Debugging:*  
	  `quasar dev` /* from root folder */
	  `dotnet watch run` // from backend folder  

*Publish:*  
	  `quasar build`  
    `dotnet publish -c Release`

  copy client output files in dist\spa folder to backend\bin\Release\net6.0\publish\wwwroot


## User Interface
### Enter the site

![1-giriş ekranı](https://github.com/timucinylmz/UX-PFB/assets/50196616/7aa760be-409c-4f11-b2d4-7e8fd72e99d3)


### Enter 8 bytes seed value and create "namespace ID" by 'calculate' button
-PFB is a PayForBlob Message.  
-The endpoint also takes in a namespace_id and data values.  
-Namespace ID should be 8 bytes.  

![namespace id](https://github.com/timucinylmz/UX-PFB/assets/50196616/0bfe2023-3fc2-4947-8e50-2709cd328a16)


### Submitting PayForBlob transaction by 'Run' button

![pfb](https://github.com/timucinylmz/UX-PFB/assets/50196616/9c1cb81a-5f49-404a-932c-cbe4dfa07392)


### View the "Block Details" on mintscan.io by clicking on the 'height' details

![block](https://github.com/timucinylmz/UX-PFB/assets/50196616/6c56c6a3-c771-45fa-af01-aa0ba123cb33)


### View the "Transaction Details" on mintscan.io by clicking on the 'Txhash' details

![txhash](https://github.com/timucinylmz/UX-PFB/assets/50196616/d6fd2e9b-53b0-490c-8989-824c36e7d365)




