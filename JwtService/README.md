#### Purpose

Speed up the JWT Authentication setup

#### How to use

1.  Add required paramaters to your appsettings.json file.
	* LifeSpan = Key expires from UTCNOW plus LifeSpan minutes
	* appsettings.json environment variables not yet supported
	
```javascript
	"JwtSettings": {
		"Secret": "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING",
		"Issuer": "http://yourdomain.com",
		"Audience": "https://yourdomain.com",
		"LifeSpan": 5
	}
```

2.  Add the following code to your Startup.cs file public void ConfigureServices:

```C
	services.ConfigureJwtAuthentication(Configuration);
``` 

3.  Add the following code to your Startup.cs file inside public void Configure: 

```C
	app.UseAuthentication();
	app.UseAuthorization();
```

4.  Register as a service, inject & use. Interface & Repository names: IJwtRepo, JwtRepo