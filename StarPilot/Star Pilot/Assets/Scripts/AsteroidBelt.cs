using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBelt : MonoBehaviour
{
    public GameObject asteroidPrefab;  // ������ ��������
    public Transform planet;           // �������, ������� ��� ���������� ��������
    public Transform asteroidBeltParent; // ������� ��'��� AsteroidBelt ��� ���������� ��������
    public int numberOfAsteroidsPerLine; // ʳ������ �������� �� ����� ��
    public int numberOfLines;      // ʳ������ ��� (��)
    public float startRadius;     // ���������� ����� (��������� ��� �� �������)
    public float radiusStep;      // ���� �� �����
    public float rotationSpeed;  // �������� ��������� ��������
    public Vector2 randomScaleRange = new Vector2(0.5f, 2f); // ĳ������ ���������� ������ ��������
    public float randomOffsetAngle; // ĳ������ ��� �������� ���� ����
    public float randomOffsetRadius; // ĳ������ ��� �������� ���� ������

    private List<GameObject> asteroids = new List<GameObject>(); // ������ ��� ���������� ��������
    private List<float> radiusList = new List<float>(); // ������ ��� ���������� ������ ������� ��������
    private List<float> initialAngles = new List<float>(); // �������� ���� ��� ������� ��������
    private float timer; // ������ ��� ����������� ���������

    void Start()
    {
        SpawnAsteroids();
    }

    // ���������� ��������� � FixedUpdate, ��� ����������� ����, �� Update.
    void FixedUpdate()
    {
        RotateAsteroids(); // ��������� ����� ��� ��������� ��������
    }

    void SpawnAsteroids()
    {
        for (int line = 0; line < numberOfLines; line++)
        {
            // ��������� ����� ��� ������� ��
            float currentRadius = startRadius + line * radiusStep;

            for (int i = 0; i < numberOfAsteroidsPerLine; i++)
            {
                // ��� ��� ������������ �������� �� ��� � ���������� ����������
                float angle = i * Mathf.PI * 2 / numberOfAsteroidsPerLine + Random.Range(-randomOffsetAngle, randomOffsetAngle);

                // ������ ���������� ���� �� ������
                float randomRadius = currentRadius + Random.Range(-randomOffsetRadius, randomOffsetRadius);

                // ���������� ������� �� ���, ������������ Y �� 0
                Vector3 spawnPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * randomRadius;

                // ������ ������� �������, ��� ����� ��������� ������� ��
                spawnPosition += new Vector3(planet.position.x, 0, planet.position.z); // Y=0 ��� ������� �������� �� ����� 0

                // ��������� �������
                GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

                // ������ ���������� ����� ��������
                float randomScale = Random.Range(randomScaleRange.x, randomScaleRange.y);
                asteroid.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                // ����������� ������� �� �������
                asteroid.transform.LookAt(new Vector3(planet.position.x, 0, planet.position.z)); // Y=0 ��� �������

                // ������ ������� �� ������� ��'��� �� AsteroidBelt
                asteroid.transform.SetParent(asteroidBeltParent);

                // �������� ������� �� ���� �����
                asteroids.Add(asteroid);
                radiusList.Add(randomRadius); // ������������� ���������� �����
                initialAngles.Add(angle); // �������� ���������� ��������� ��� ��� ���������
            }
        }
    }

    void RotateAsteroids()
    {
        for (int i = 0; i < asteroids.Count; i++)
        {
            GameObject asteroid = asteroids[i];
            float radius = radiusList[i];
            float initialAngle = initialAngles[i];

            // ���������� ����� ��� ��������� ������� �������
            float angle = initialAngle + Time.time * rotationSpeed / radius;

            // ���������� ���� ������� �� ���
            Vector3 newPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            // ������ ������� �������
            newPosition += new Vector3(planet.position.x, 0, planet.position.z);

            // ������ ���� ������� ��������
            asteroid.transform.position = newPosition;

            // ��������� ������� �� �������
            asteroid.transform.LookAt(new Vector3(planet.position.x, 0, planet.position.z));
        }
    }
}
