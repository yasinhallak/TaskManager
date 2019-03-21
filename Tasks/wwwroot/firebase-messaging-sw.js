importScripts('https://www.gstatic.com/firebasejs/5.7.0/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/5.7.0/firebase-messaging.js');

// Initialize Firebase
var config = {
    apiKey: "AIzaSyAwASfjWUoAJ0gE4k9HhLmBnKZUVWA__XY",
    authDomain: "fir-adamtask.firebaseapp.com",
    databaseURL: "https://fir-adamtask.firebaseio.com",
    projectId: "fir-adamtask",
    storageBucket: "fir-adamtask.appspot.com",
    messagingSenderId: "550075842685"
};
firebase.initializeApp(config);
var messaging = firebase.messaging();
messaging.setBackgroundMessageHandler(function (payload) {
    console.log('[firebase-messaging-sw.js] Received background message ', payload);
    // Customize notification here
    var notificationTitle = 'Background Message Title';
    var notificationOptions = {
        body: 'Background Message body.',
        icon: '/firebase-logo.png'
    };

    return self.registration.showNotification(notificationTitle,
        notificationOptions);
});
// [END background_handler]