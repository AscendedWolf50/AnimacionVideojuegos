# AnimacionVideojuegos

# Entrega 2 - TPS Controller 

## Contenido

Este proyecto implementa un **TPS Controller** en Unity con tres personajes jugables, cámara TPS + Aim con Cinemachine, sistema de disparo hitscan con recoil, y un sistema de **Lock-On con conmutación de objetivo y persistencia**.

---

## Controles

* **WASD** → Movimiento del personaje
* **Mouse** → Control de cámara 
* **Click derecho (Hold)** → Apuntar 
* **Click izquierdo** → Disparar 
* **Botón central del mouse → Activar/Desactivar Lock-On
* **Q** → Cambiar objetivo cuando hay lock-on

---

## Personajes Jugables

Existen **3 personajes distintos**, con diferencias claras en fire rate, recoil y zoom:

1. **Asalto (estándar) (Llamado "Ninja" dentro de la escena)**

   * Fire Rate: medio
   * Recoil: normal
   * Zoom: estándar (al hombro)
   * Sensación de fusil de asalto

2. **Pesado (sniper) (Llamado "Soldier" dentro de la escena)**

   * Fire Rate: muy bajo
   * Recoil: muy fuerte
   * Zoom: alto (más cerrado)
   * Sensación de francotirador

3. **Ligero (minigun) (Llamado "Alien" dentro de la escena)**

   * Fire Rate: muy alto
   * Recoil: elevado pero sostenido
   * Zoom: más abierto (para simular arma grande)
   * Sensación de ametralladora pesada/minigun

---

## Cómo correrlo

1. Clonar el repositorio y cambiar a la rama **`entrega-2`**.
2. Abrir el proyecto en Unity **60000.0.43f1 LTS**.
3. Ir a la carpeta `Assets/Entrega2` y abrir la escena **Entrega2**.
4. La escena ya incluye varios enemigos y un muro de prueba para comprobar:

   * Disparo hitscan
   * Conmutación de objetivo en Lock-On
   * Persistencia del lock cuando hay obstáculos
5. Dar Play en el editor y seleccionar uno de los prefabs/personajes incluidos.

---

## Profundización Elegida

### 1. Conmutación de objetivo en Lock-On (Q)

* **Qué hace:** permite alternar entre múltiples objetivos válidos cuando el lock está activo, usando la tecla **Q**.
* **Cómo probarlo:**

  1. Activar lock-on con el **botón central del mouse** sobre un enemigo.
  2. Presionar **Q** → el objetivo va rotando entre los enemigos visibles en escena.


### 2. Persistencia del Lock-On (line of sight)

* **Qué hace:** si un obstáculo (objeto en el layer "obstacle" se interpone entre la cámara y el objetivo, el lock se pierde automáticamente.
* **Cómo probarlo:**

  1. Activar lock-on sobre un enemigo.
  2. Moverse detrás del muro en la escena.
  3. El lock se libera automáticamente.


---

## Video / Gif

[Enlace al video de demostración](TU_LINK_AQUI)
(Muestra: selección de los 3 personajes, diferencias en disparo y zoom, lock-on básico, conmutación de objetivos y persistencia con obstáculos).

---


