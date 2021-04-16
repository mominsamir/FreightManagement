const CracoLessPlugin = require("craco-less-plugin");
const { getThemeVariables } = require("antd/dist/theme");

module.exports = {
    plugins: [
        {
            plugin: CracoLessPlugin,
            options: {
                modifyVars: getThemeVariables({
                    dark: true,
                    compact: false,
                    'primary-color': '#1890ff',
                    'font-size-base': '20px'
                }),
                javascriptEnabled: true,
            },
        },
    ],
};
