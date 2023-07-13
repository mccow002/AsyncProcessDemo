import { TestBed } from '@angular/core/testing';

import { ConnectionIdInterceptor } from './connection-id.interceptor';

describe('ConnectionIdInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      ConnectionIdInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: ConnectionIdInterceptor = TestBed.inject(ConnectionIdInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
