## Equipment Rental Midterm

---
By **Gabriel Siewert and Dahyun Ko** <br>
Link to video presentation: 
## Instructions to run the API

---
All users must be authorized to perform any Get or Post actions.
After starting the application, use Postman to get a token.
Use the URL ```/api/auth/login``` on top of your local host port and make sure to include sign in credentials in the body.
Sign in credentials format:
```json
{
    "username": "",
    "password": ""
}
```
The default credentials for admin and user are:
<br>
**Admin** &nbsp;= Username: ```Dany``` Password: ```Danypassword``` <br>
**User**&nbsp;&nbsp;&nbsp;&nbsp; = Username: ```Gabe``` Password: ```Gabepassword``` <br>

After sending the Post request, you will receive a token.<br>Copy it, and paste it under "Authorization -> Bearer Token"
Now you will be authorized to make requests see Equipment, Customers, and Rentals (your role will determine which HTTP requests you can access).

Equipment  &nbsp;  &nbsp; /api/equipment     &nbsp; &nbsp;     Get all Equipment <br>
Customers  &nbsp;  &nbsp; /api/customers     &nbsp; &nbsp;     Get all Customers <br>
Rentals  &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; /api/rentals     &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    Get all Rentals <br>
<br>
The rest of the endpoints work as expected as per the assignment instructions.<br>

## AI Assistance Used

---
All backend logic was created and tested without any use of AI assistance. <br>
We used AI assistance for the following:<br>
- Generating razor pages for the front end.<br>
- To help us understand how to connect the backend API to the views.<br>
- We ended up creating a service layer that connected the API and Views controllers. <br>
- **We used AI to help is learn & understand how to create the service layer and connect the API to the views, and NOT to implement a solution for us.** <br>