window.login = async (model) => {
  const response = await fetch("/auth/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    credentials: "include",
    body: JSON.stringify(model)
  });

  return response.ok;
};

window.logout = async () => {
  const response = await fetch("/auth/logout", {
    method: "POST",
    credentials: "include"
  });
  return response.ok;
};
