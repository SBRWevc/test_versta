function togglePasswordVisibility() {
    const passwordInput = document.getElementById('passwordInput');
    const passwordIcon = document.getElementById('passwordIcon');
    const isPasswordVisible = passwordInput.type === 'text';
    passwordInput.type = isPasswordVisible ? 'password' : 'text';
    passwordIcon.classList.toggle('bi-eye', !isPasswordVisible);
    passwordIcon.classList.toggle('bi-eye-slash', isPasswordVisible);
}

function toggleConfirmPasswordVisibility() {
    const passwordInput = document.getElementById('confirmPasswordInput');
    const passwordIcon = document.getElementById('confirmPasswordIcon');
    const isPasswordVisible = passwordInput.type === 'text';
    passwordInput.type = isPasswordVisible ? 'password' : 'text';
    passwordIcon.classList.toggle('bi-eye', !isPasswordVisible);
    passwordIcon.classList.toggle('bi-eye-slash', isPasswordVisible);
}

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/ordersHub")
    .build();

connection.on("ReceiveNewOrder", function(order) {
    const pickupDate = new Date(order.pickupDate).toLocaleDateString();
    const orderHtml = `
            <div class="col-12 col-md-6 col-lg-4 mb-4" id="order-${order.orderId}">
                <div class="card animate__animated animate__fadeInUp">
                    <div class="card-header">
                        Заказ № ${order.orderId}
                    </div>
                    <div class="card-body">
                        <p class="card-text">
                            <strong>Отправитель:</strong> ${order.senderCity}, ${order.senderAddress}<br />
                            <strong>Получатель:</strong> ${order.recipientCity}, ${order.recipientAddress}<br />
                            <strong>Вес груза:</strong> ${order.weight} кг<br />
                            <strong>Дата забора:</strong> ${pickupDate}
                        </p>
                        <a href="/Orders/Details/${order.orderId}" class="btn btn-primary">Подробнее</a>
                    </div>
                </div>
            </div>
        `;
    document.getElementById("ordersRow").insertAdjacentHTML("afterbegin", orderHtml);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});