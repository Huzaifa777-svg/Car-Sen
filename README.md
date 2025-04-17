Bilkul Huzaifa! Chalo is **Unity C# script** ko line-by-line, word-by-word Urdu/English mix mein **step-by-step** samajhtay hain. Ye script ek **car controller** ke liye likhi gayi hai — jisme humari car move, steer, aur brake karti hai.

---

### 🔷 `using System.Collections;`  
Ye line .NET ka ek **namespace** include karti hai jo collections jese **List**, **ArrayList** waghera ke liye hota hai. Hum yahan direct use nahi kar rahe, but safe to include.

---

### 🔷 `using System.Collections.Generic;`
Ye bhi collections ke liye hai — thora advanced version jo generics (<> angle brackets wale) allow karta hai. Again, yahan direct use nahi hua but commonly included hota hai.

---

### 🔷 `using UnityEngine;`
Ye line Unity ke **core engine** ko include karti hai — bina iske aap `MonoBehaviour`, `GameObject`, `Transform` waghera use nahi kar saktay.

---

### 🔷 `public class PlayerCarController : MonoBehaviour`
- **public class**: Ye ek **class** define kar raha hai, naam `PlayerCarController`.
- `MonoBehaviour`: Iska matlab hai ke ye Unity ka component hai — is class ko aap kisi **GameObject** ke saath attach kar saktay ho.

---

## 🛞 Wheel Setup

### 🔷 `[Header("Wheels collider")]`
- Unity inspector mein ek **label** show karta hai.
- Visual grouping ka kaam karta hai.

### 🔷 WheelCollider variables:
```csharp
public WheelCollider frontLeftWheelCollider;
public WheelCollider frontRightWheelCollider;
public WheelCollider backLeftWheelCollider;
public WheelCollider backRightWheelCollider;
```
- Ye car ke chaar wheels ke **colliders** hain — yeh physics ke liye hotay hain.
- Inse car ka **movement** calculate hota hai.

---

### 🔷 `[Header("Wheels Transform")]` + `Transform` variables:
Transform means **position/rotation/scale** in Unity space. Ye wheels ki **visual** movement ke liye hain.

---

## 🔧 Car Engine & Steering

### 🔷 `[Header("Car Engine")]`
```csharp
public float accelerationForce = 300f;
public float breakingForce = 3000f;
public float presentBreakForce = 0f;
public float presentAcceleration = 0f;
```
- `accelerationForce`: Jab user **upar arrow** dabaye to itni power se car aagay jayegi.
- `breakingForce`: Brakes lagane ki strength.
- `presentBreakForce`: Abhi brake laga hai ya nahi.
- `presentAcceleration`: Jo bhi moment par acceleration lag rahi hai.

---

### 🔷 `[Header("Car Steering")]`
```csharp
public float WheelsTorque = 35f;
private float presentTurnAngle = 0f;
```
- `WheelsTorque`: Turning ka max angle.
- `presentTurnAngle`: Abhi actual kitna angle turn ho raha hai.

---

## 🌀 `Update()` Function

```csharp
private void Update()
{
    MoveCar();
    CarSteering();
    ApplyBreaks();
}
```
- Unity ka built-in method jo **har frame** mein call hota hai.
- Har frame mein car move hogi, turn hogi, aur brake check hoga.

---

## 🚗 MoveCar()

```csharp
private void MoveCar()
{
    frontLeftWheelCollider.motorTorque = presentAcceleration;
    frontRightWheelCollider.motorTorque = presentAcceleration;
    backLeftWheelCollider.motorTorque = presentAcceleration;
    backRightWheelCollider.motorTorque = presentAcceleration;

    presentAcceleration = accelerationForce * Input.GetAxis("Vertical");
}
```

- `motorTorque`: Force jo wheel par lagti hai (jaise acceleration).
- `Input.GetAxis("Vertical")`: User agar **upar ya neeche arrow key** dabaye to ye +1 ya -1 return karta hai.
- Aakhir mein presentAcceleration update hoti hai.

---

## 🕹️ CarSteering()

```csharp
presentTurnAngle = WheelsTorque * Input.GetAxis("Horizontal");
```
- `Input.GetAxis("Horizontal")`: Left/Right arrow keys.
- Car ke front wheels ko steering angle diya jata hai.

### `streeingwheels()` function:
```csharp
void streeingwheels(WheelCollider WC, Transform WT)
```
- Ye har wheel ka **visual transform** update karta hai taake wo sahi dikhe.
- `GetWorldPose()` se position & rotation milti hai wheel ki.

---

## 🛑 ApplyBreaks()

```csharp
if(Input.GetKey(KeyCode.Space))
    presentBreakForce = breakingForce;
else
    presentBreakForce = 0f;
```
- Agar **space bar** dabaya gaya hai to brake force lagao.
- Warna brake force 0 rakho.

---

### Brake Torque apply hoti hai:
```csharp
frontLeftWheelCollider.brakeTorque = presentBreakForce;
...
```
- Har wheel par torque apply ki jati hai jab brake lagti hai.

---

## ❗ Typos / Improvements
1. Function name `streeingwheels` ko `SteeringWheels` bana do for better readability.
2. `WheelsTorque` → `wheelSteerAngle` jese naam aur clear hoga.
3. Thoda code cleanup aur comments se aur bhi readable ban sakta hai.

---
