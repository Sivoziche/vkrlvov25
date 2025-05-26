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
        // �������� ������������� �������
        GameObject shipGO = new GameObject("Spaceship");
        var ship = shipGO.AddComponent<SpaceshipTest>(); // ������ ���������� ����� �����

        GameObject planetGO = new GameObject("Planet");
        var planet = planetGO.AddComponent<GravityBody>(); // � ���� �����

        Assert.IsNotNull(ship);
        Assert.IsNotNull(planet);
    }
}
