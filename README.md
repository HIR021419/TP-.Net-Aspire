# 🧩 Projet .NET Aspire — Microservices avec Dapr et Kubernetes

## 📘 Description

Ce projet illustre la mise en place d’une architecture microservices avec **.NET Aspire**, orchestrée à l’aide de **Dapr** pour la communication inter-services et déployée sur un cluster **Kubernetes local** (via Rancher Desktop).

L’objectif est de fournir un environnement de développement et de déploiement complet, de la création des services jusqu’à leur déploiement sur un cluster K8S local.

---

## 🏗️ Structure du projet

```
📦 MicroServices.Aspire
├── AppHost/              # Projet principal Aspire
│   └── Program.cs        # Définition des ressources et dépendances
├── UserService/          # Microservice de gestion des utilisateurs
├── TodoService/          # Microservice de gestion des tâches (exemple)
├── docker-compose.yml    # Lancement local (optionnel)
└── README.md             # Documentation du projet
```

---

## ⚙️ Prérequis

### Outils nécessaires

* [.NET 8 SDK ou supérieur](https://dotnet.microsoft.com/en-us/download)
* [Docker Desktop ou Rancher Desktop](https://rancherdesktop.io/)
* [kubectl](https://kubernetes.io/docs/tasks/tools/)
* [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/)
* [Aspirate](https://github.com/bunit-dev/aspirate)

### Installation de Dapr CLI

```bash
dotnet tool install -g dapr-cli
dapr init
```

### Installation d’Aspirate

```bash
dotnet tool install -g aspirate --prerelease
```

---

## 🚀 Exécution locale avec Aspire

Depuis le dossier `AppHost`, exécute :

```bash
dotnet run
```

Aspire va lancer l’orchestrateur et les différents microservices.

---

## ☸️ Déploiement Kubernetes avec Aspirate

### 1. Vérifier le cluster Kubernetes

Assurez-vous que Rancher Desktop est configuré avec Kubernetes actif.

```bash
kubectl get nodes
```

Si le cluster répond, vous devriez voir un nœud actif.

### 2. Démarrer un registre Docker local

```bash
docker run -d -p 5001:5000 --restart always --name registry registry:2
```

Cela hébergera les images Docker localement.

### 3. Génération du manifeste Aspire

Depuis le projet `AppHost` :

```bash
dotnet run --publisher manifest --output-path manifest.json
```

### 4. Initialiser Aspirate

```bash
aspirate init
```

* **Registry** : `localhost:5001`
* **Repository Prefix** : votre nom utilisateur Docker (ex : `luciebouvard`)

Cela crée un fichier `aspirate.json` contenant la configuration du déploiement.

### 5. Construire les images Docker

```bash
aspirate build
```

Les images sont construites et poussées dans le registre local.

### 6. Générer les fichiers YAML Kubernetes

```bash
aspirate generate
```

Un dossier `generated/` est créé avec les manifests pour chaque service.

### 7. Appliquer le déploiement sur le cluster

```bash
aspirate apply
```

Si Dapr n’est pas installé dans le cluster, Aspirate le déploiera automatiquement.

### 8. Vérifier le déploiement

```bash
kubectl get pods -n demo
```

Les pods devraient apparaître en état **Running**.

---

## 🧩 Debug & gestion Dapr

Vérifier le statut de Dapr dans le cluster :

```bash
dapr status -k
```

Consulter les logs d’un microservice :

```bash
kubectl logs -f <nom-du-pod>
```

---

## 🧭 Accès aux services

En développement, vous pouvez rediriger les ports pour accéder à vos API :

```bash
kubectl port-forward svc/userservice 8080:80 -n demo
```

Ensuite, accédez à votre API sur :

```
http://localhost:8080/users
```

---

## 🧹 Nettoyage du déploiement

Pour supprimer le namespace et les ressources :

```bash
kubectl delete namespace demo
```

---

## 🧠 Notes importantes

* Le fichier `aspirate.json` ne doit **jamais** contenir `OutputFormat: docker`. Cette valeur provoque une erreur.
* Vérifiez toujours que `manifest.json` existe avant d’exécuter `aspirate build` ou `generate`.
* Le registre local (`localhost:5001`) doit être actif pour permettre le push des images.

---

## 📚 Références

* [Documentation officielle .NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/)
* [Documentation Dapr](https://docs.dapr.io/)
* [Aspirate GitHub](https://github.com/bunit-dev/aspirate)
* [Rancher Desktop](https://rancherdesktop.io/)

---

✅ **Auteur :** Yann Lemétayer
🗓️ **Dernière mise à jour :** Octobre 2025
