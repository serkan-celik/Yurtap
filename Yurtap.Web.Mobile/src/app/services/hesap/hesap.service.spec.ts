import { TestBed } from '@angular/core/testing';

import { HesapService } from './hesap.service';

describe('HesapService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HesapService = TestBed.get(HesapService);
    expect(service).toBeTruthy();
  });
});
