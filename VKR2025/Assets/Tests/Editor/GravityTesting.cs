using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GravityTesting
{
    [Test]
    public void SpaceshipAndGravityBodyExist()
    {
        // Проверка существования классов
        GameObject shipGO = new GameObject("Spaceship");
        var ship = shipGO.AddComponent<SpaceshipTest>(); // Теперь компилятор видит класс

        GameObject planetGO = new GameObject("Planet");
        var planet = planetGO.AddComponent<GravityBody>(); // И этот класс

        Assert.IsNotNull(ship);
        Assert.IsNotNull(planet);
    }
}
