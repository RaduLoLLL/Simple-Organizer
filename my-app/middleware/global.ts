// file: ~/middleware/auth.global.ts
export default defineNuxtRouteMiddleware(async (to) => {
  // 1. Always allow access to the login page
  const { status } = await useAuth();
  if (to.path === "/") {
    return;
  }
  // 2. Otherwise: Check status and redirect to login page

  if (status.value !== "authenticated") {
    navigateTo("/");
  }
});
