#pragma strict

var allEnemy:GameObject[];
var shield:GameObject ;
var thunder:GameObject ;
var damage:GameObject ;

function Start () {
	
}

function Update () {

	if(Input.GetKeyDown("5"))
	{
		Instantiate(shield,transform.position,transform.rotation);
	}
	if(Input.GetKeyDown("6"))
	{
		allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
		for(var enemy : GameObject in allEnemy)
		{
			Instantiate(thunder, enemy.transform.position, enemy.transform.rotation);
        }
	}
	if(Input.GetKeyDown("9"))
	{
		allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
		for(var enemy : GameObject in allEnemy)
		{
			Instantiate(damage, enemy.transform.position, enemy.transform.rotation);

        }
	}

}