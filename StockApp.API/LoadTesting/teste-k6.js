import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 50,
    duration: '30s',
};

export default function () {

    let url = 'https://localhost:7189/api/Categories';

    let res = http.get(url);

    check(res, {
        'status é 200': (r) => r.status === 200,
        'respondeu em menos de 500ms': (r) => r.timings.duration < 500,
    });

    sleep(1); 
}
