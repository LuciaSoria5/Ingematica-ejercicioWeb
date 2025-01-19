function sendMail(event) {

    event.preventDefault();

    const fromName = document.getElementById("from_name").value.trim();
    const email = document.getElementById("email").value.trim();
    const message = document.getElementById("message").value.trim();

    const nameRegex = /^[a-zA-ZÀ-ÿ\s'-]+$/; // Letras, espacios, apóstrofes y guiones
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Formato de email básico

    // Validar nombre
    if (!nameRegex.test(fromName)) {
        alert("Por favor, ingresa un nombre válido (solo letras, espacios, apóstrofes o guiones).");
        return;
    }

    // Validar email
    if (!emailRegex.test(email)) {
        alert("Por favor, ingresa un correo electrónico válido.");
        return;
    }

    // Validar mensaje
    if (message.length === 0) {
        alert("Por favor, ingresa un mensaje.");
        return;
    }

    // Enviar correo
    emailjs.send("service_nw8suuk", "template_8i2njps", {
        from_name: fromName,
        message: message,
        email: email,
    }).then(
        () => {
            alert("Email enviado exitosamente.");
            document.getElementById("from_name").value = '';
            document.getElementById("email").value = '';
            document.getElementById("message").value = '';
        },
        (error) => {
            console.error("Error al enviar el correo:", error);
            alert("Hubo un error al enviar el correo. Inténtalo nuevamente.");
        }
    );
}
