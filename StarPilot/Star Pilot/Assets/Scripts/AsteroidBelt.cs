using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBelt : MonoBehaviour
{
    public GameObject asteroidPrefab;  // Префаб астероїда
    public Transform planet;           // Планета, навколо якої спавняться астероїди
    public Transform asteroidBeltParent; // Порожній об'єкт AsteroidBelt для збереження астероїдів
    public int numberOfAsteroidsPerLine; // Кількість астероїдів на кожній лінії
    public int numberOfLines;      // Кількість ліній (кіл)
    public float startRadius;     // Початковий радіус (найближча лінія до планети)
    public float radiusStep;      // Крок між лініями
    public float rotationSpeed;  // Швидкість обертання астероїдів
    public Vector2 randomScaleRange = new Vector2(0.5f, 2f); // Діапазон випадкових розмірів астероїдів
    public float randomOffsetAngle; // Діапазон для хаотичної зміни кута
    public float randomOffsetRadius; // Діапазон для хаотичної зміни радіусу

    private List<GameObject> asteroids = new List<GameObject>(); // Список для збереження астероїдів
    private List<float> radiusList = new List<float>(); // Список для збереження радіусів кожного астероїда
    private List<float> initialAngles = new List<float>(); // Початкові кути для кожного астероїда
    private float timer; // Таймер для фіксованого обертання

    void Start()
    {
        SpawnAsteroids();
    }

    // Переносимо обертання у FixedUpdate, яке викликається рідше, ніж Update.
    void FixedUpdate()
    {
        RotateAsteroids(); // Викликаємо метод для обертання астероїдів
    }

    void SpawnAsteroids()
    {
        for (int line = 0; line < numberOfLines; line++)
        {
            // Визначаємо радіус для поточної лінії
            float currentRadius = startRadius + line * radiusStep;

            for (int i = 0; i < numberOfAsteroidsPerLine; i++)
            {
                // Кут для розташування астероїдів на колі з випадковим відхиленням
                float angle = i * Mathf.PI * 2 / numberOfAsteroidsPerLine + Random.Range(-randomOffsetAngle, randomOffsetAngle);

                // Додаємо випадковий зсув до радіусу
                float randomRadius = currentRadius + Random.Range(-randomOffsetRadius, randomOffsetRadius);

                // Обчислюємо позицію на колі, встановлюючи Y на 0
                Vector3 spawnPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * randomRadius;

                // Додаємо позицію планети, щоб спавн відбувався навколо неї
                spawnPosition += new Vector3(planet.position.x, 0, planet.position.z); // Y=0 для підняття астероїдів на рівень 0

                // Створюємо астероїд
                GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

                // Задаємо випадковий розмір астероїда
                float randomScale = Random.Range(randomScaleRange.x, randomScaleRange.y);
                asteroid.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

                // Направляємо астероїд на планету
                asteroid.transform.LookAt(new Vector3(planet.position.x, 0, planet.position.z)); // Y=0 для планети

                // Додаємо астероїд як дочірній об'єкт до AsteroidBelt
                asteroid.transform.SetParent(asteroidBeltParent);

                // Зберігаємо астероїд та його радіус
                asteroids.Add(asteroid);
                radiusList.Add(randomRadius); // Використовуємо випадковий радіус
                initialAngles.Add(angle); // Зберігаємо початковий хаотичний кут для обертання
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

            // Обчислюємо новий кут обертання навколо планети
            float angle = initialAngle + Time.time * rotationSpeed / radius;

            // Обчислюємо нову позицію на колі
            Vector3 newPosition = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            // Додаємо позицію планети
            newPosition += new Vector3(planet.position.x, 0, planet.position.z);

            // Задаємо нову позицію астероїду
            asteroid.transform.position = newPosition;

            // Повертаємо астероїд до планети
            asteroid.transform.LookAt(new Vector3(planet.position.x, 0, planet.position.z));
        }
    }
}
