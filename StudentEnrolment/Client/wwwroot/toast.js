function showToast() {
    var toastTrigger = document.getElementById('liveToastBtn');
    var toastLiveExample = document.getElementById('liveToast');

    if (toastLiveExample) {
        var toast = new bootstrap.Toast(toastLiveExample);
        toast.show();
    } else {
        console.error("Toast element not found.");
    }
}