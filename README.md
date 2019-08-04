Example POST Request for "/api/types/dog"
{
  "name": "Dog",
  "attributes": ["Name",  "Breed" ],
  "required" : ["Name"],
  "maxLength" : {
  	"Name" : "30",
  	"Breed" : "50"
  },
  "minLength" : {
  	"Name" : "3"
  }
}
Example POST Request for "/api/entities/dog"
{
	"attributes":
	{
		"Name" :"Lucy",
		"Breed": "Alabai"
	}
}

