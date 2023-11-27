//using UnityEngine;
//using UnityEngine.UI;

//public class ToggleController : MonoBehaviour
//{
//    public GameObject prefabToToggle;
//    public Toggle toggle;

//    private GameObject instantiatedPrefab; // Prefab yang telah di-instantiate di scene

//    private void Start()
//    {
//        // Mengecek apakah prefabToToggle sudah di-instantiate di scene
//        instantiatedPrefab = GameObject.Find(prefabToToggle.name);

//        // Mengatur toggle agar sesuai dengan status awal instantiatedPrefab (jika sudah ada di scene)
//        if (instantiatedPrefab != null)
//        {
//            toggle.isOn = instantiatedPrefab.activeSelf;
//        }
//        // Menambahkan listener pada toggle
//        toggle.onValueChanged.AddListener(OnToggleValueChanged);
//    }

//    private void OnToggleValueChanged(bool newValue)
//    {
//        // Jika prefab belum di-instantiate, instantiate prefabToToggle di scene
//        if (instantiatedPrefab == null)
//        {
//            instantiatedPrefab = Instantiate(prefabToToggle);
//            instantiatedPrefab.name = prefabToToggle.name; // Memberikan nama yang sama dengan prefab
//        }

//        // Mengubah status active atau inactive sesuai dengan nilai toggle
//        instantiatedPrefab.SetActive(newValue);

//        // Mengatur kondisi hide dan unhide berdasarkan nilai toggle
//        // Di sini, contoh menggunakan renderer untuk hide/unhide
//        Renderer prefabRenderer = instantiatedPrefab.GetComponent<Renderer>();
//        if (prefabRenderer != null)
//        {
//            prefabRenderer.enabled = newValue;
//        }
//        // Lakukan operasi lain untuk menampilkan atau menyembunyikan prefab jika diperlukan
//    }
//}