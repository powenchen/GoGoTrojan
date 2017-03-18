#pragma strict

var bullet:GameObject;
var allEnemy:GameObject[];

var missil:GameObject ;
var mine:GameObject ;
var oil:GameObject ;
var glue:GameObject ;
var shield:GameObject ;
var thunder:GameObject ;
var damage:GameObject ;

function Start () {
	
}

function Update () {

	if(Input.GetKeyDown("j"))
	{
		Instantiate(bullet,transform.position,transform.rotation);
	
	}

	if(Input.GetKeyDown("k"))
	{
		allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
		for(var enemy : GameObject in allEnemy)
		{
			Instantiate(thunder, enemy.transform.position, enemy.transform.rotation);
        }
	}

	if(Input.GetKeyDown("1"))
	{
		allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
		for(var enemy : GameObject in allEnemy)
		{
			Instantiate(missil, enemy.transform.position, enemy.transform.rotation);
        }
	}
	if(Input.GetKeyDown("2"))
	{
		allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
		for(var enemy : GameObject in allEnemy)
		{
			Instantiate(mine, enemy.transform.position, enemy.transform.rotation);
        }
	}
	if(Input.GetKeyDown("3"))
	{
		allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
		for(var enemy : GameObject in allEnemy)
		{
			Instantiate(oil, enemy.transform.position, enemy.transform.rotation);
        }
	}
	if(Input.GetKeyDown("4"))
	{
		allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
		for(var enemy : GameObject in allEnemy)
		{
			Instantiate(glue, enemy.transform.position, enemy.transform.rotation);
        }
	}
	if(Input.GetKeyDown("5"))
	{
		allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
		for(var enemy : GameObject in allEnemy)
		{
			Instantiate(shield, enemy.transform.position, enemy.transform.rotation);
        }
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