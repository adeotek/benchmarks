'use strict';

import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";
import { textSummary } from 'https://jslib.k6.io/k6-summary/0.0.2/index.js';
import http from 'k6/http'
import { check, sleep } from 'k6';

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    vus: 20,
    iterations: 30,
    duration: '30s',
    summaryTimeUnit: 'ms'
};

export default () => {
    const url = 'https://localhost:7030/weatherforecast?days=5';

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const response = http.get(url, params);
    check(response, { 'status was 200': (r) => r.status === 200 });
    sleep(1);
}

export function handleSummary(data) {
    return {
        'stdout': textSummary(data, { indent: ' ', enableColors: true }),
        'summary.json': JSON.stringify(data),
        "summary.html": htmlReport(data)
    };
}