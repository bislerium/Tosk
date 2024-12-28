window.setBootstrapTheme = function setBootstrapTheme(theme) {
    document
        .documentElement
        .setAttribute("data-bs-theme", theme.toLowerCase());
    console.log(`Theme changed to: ${theme}`);
}